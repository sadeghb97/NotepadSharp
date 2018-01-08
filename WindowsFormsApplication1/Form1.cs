using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class Frmmain : Form
    {
        myUndo undoClass = new myUndo();
        string fn;
        string originalStr;
        int mainFormHeight, mainFormWidth;
        Boolean statusbarVisible;
        //Moteghayyeri ke neshan midahad statusbar visible ya unvisible ast hata zamani ke statusbar enable
        //nabashad. faghat zamani ke statusbar khasiate checked an taghir konad bayad set shavad.
        Boolean SaveFlag;
        Boolean newToolStripMenuItem_Click_Flag;
        string searchString;
        string replaceString;
        Boolean searchDown;
        StringComparison searchMatchCase;
        Boolean textChangedUndoSetText;
        //zamani ke selectionLength sefr nist va dokme backspace feshordeh mishavad ghabl az pak shodan
        //yek bar settext dar keypress va sepas be komake in moteghayyer yek bar dar textchanged farakhani
        //khahad shod.
        //string logsPath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        string logsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NotepadSharp\\";
        string logsFileName = "log.txt";
        string logsPath;
        string LogIn;
        public void setUndoChecked(Boolean b) { undoToolStripMenuItem.Enabled = b; }
        public void setRedoChecked(Boolean b) { redoToolStripMenuItem.Enabled = b; }
        public string sendSearchString()
        {
            if (searchString != null)
                return searchString;
            return null;
        }
        public string sendReplaceString()
        {
            if (replaceString != null)
                return replaceString;
            return null;
        }
        public void ZeroTxtgeneralSelection()
        {
            txtgeneral.SelectionStart = 0;
            txtgeneral.SelectionLength = 0;
        }
        public int sendCurrentLine()
        {
            return txtgeneral.GetLineFromCharIndex(txtgeneral.SelectionStart)+1;
        }
        public int sendNumberOfLines()
        {
            return txtgeneral.GetLineFromCharIndex(txtgeneral.Text.Length) + 1;
        }
        private void setTitle()
        {
            if (fn == null)
                this.Text = "Untitled - Notepad#";
            else
            {
                int j = -1;
                int i = fn.IndexOf('\\');
                if (i != -1) j = i;
                while (i!=-1)
                {
                    i = fn.IndexOf('\\',i+1);
                    if (i != -1) j = i;
                }
                this.Text = fn.Substring(j + 1)+" - Notepad#";
            }
        }
        private int sendIndexTitle()
        {
            if (fn == null)
                return -1;
            else
            {
                int j = -1;
                int i = fn.IndexOf('\\');
                if (i != -1) j = i;
                while (i != -1)
                {
                    i = fn.IndexOf('\\', i + 1);
                    if (i != -1) j = i;
                }
                return j + 1;
            }
        }
        
        private string sendTitle()
        {
            int index = sendIndexTitle();
            if (index == -1)
                return null;
            else
                return fn.Substring(index);
        }

        private string sendDirectory()
        {
            int index = sendIndexTitle();
            if (index == -1)
                return null;
            else
                return fn.Substring(0, index - 1);
        }
        public Encoding returnFileEncoding(string filePath)
        {
            byte[] bom = new byte[5];
            System.IO.FileStream file = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            file.Read(bom, 0, 5);
            file.Close();

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            else return Encoding.Default; //ANSI
        }

        public Boolean isHaveCharacter(string str)
        {
            foreach (char c in str)
            {
                if (Char.IsLetterOrDigit(c) || Char.IsSymbol(c))
                    return true;
            }
            return false;
        }

        private Encoding sendEncodingFromMenu()
        {
            ToolStripMenuItem checkedMenu=new ToolStripMenuItem();
            foreach(ToolStripMenuItem Menu in encodingToolStripMenuItem.DropDownItems)
            {
                if (Menu.Checked == true)
                {
                    checkedMenu = Menu;
                    break;
                }
            }
            if (checkedMenu.Text == "ANSI") return Encoding.Default;
            else if (checkedMenu.Text == "Unicode") return Encoding.Unicode;
            else if (checkedMenu.Text == "Unicode big endian") return Encoding.BigEndianUnicode;
            else if (checkedMenu.Text == "UTF-8") return Encoding.UTF8;
            else if (checkedMenu.Text == "UTF-7") return Encoding.UTF7;
            else if (checkedMenu.Text == "UTF-32") return Encoding.UTF32;
            else return null;
            //Halate Akhar Rokh nemidahad faghat gozashte shode ta barname erore campail nadahad.
        }
        private void setEncodingMenuCheckedFromEncodings(Encoding enc)
        {
            if (enc == Encoding.Default) encodingsCheckedFunction(aNSIToolStripMenuItem, null);
            else if (enc == Encoding.Unicode) encodingsCheckedFunction(unicodeToolStripMenuItem, null);
            else if (enc == Encoding.BigEndianUnicode) encodingsCheckedFunction(bigToolStripMenuItem, null);
            else if (enc == Encoding.UTF8) encodingsCheckedFunction(uTF8ToolStripMenuItem, null);
            else if (enc == Encoding.UTF7) encodingsCheckedFunction(uTF7ToolStripMenuItem, null);
            else if (enc == Encoding.UTF32) encodingsCheckedFunction(uTF32ToolStripMenuItem, null);
            else throw new System.Exception();
        }
        private void StoreSaveAndOpenDialogSettings()
        {
            NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath = sendDirectory();
            NotepadSharp.Properties.Settings.Default.Save();
        }
        private string sendCurrentTimeAndDate()
        {
            string temp = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()+ ":" + DateTime.Now.Second.ToString()
                + " " + DateTime.Now.ToString("tt") + " " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            return temp;
        }
        private void LinesGotoActiverAndDeactiver()
        {
            if (txtgeneral.GetLineFromCharIndex(txtgeneral.Text.Length) > 0) gotoToolStripMenuItem.Enabled = true;
            else gotoToolStripMenuItem.Enabled = false;
        }
        private void txtmainActiverAndDeactiver()
        {
            if (txtgeneral.Text == "")
            {
                findToolStripMenuItem.Enabled = false;
                fIndNextToolStripMenuItem.Enabled = false;
            }
            else
            {
                findToolStripMenuItem.Enabled = true;
                fIndNextToolStripMenuItem.Enabled = true;
            }

            if (txtgeneral.SelectionStart == txtgeneral.Text.Length) deleteToolStripMenuItem.Enabled = false;

            if (Clipboard.ContainsText()) pasteToolStripMenuItem.Enabled = true;
            else pasteToolStripMenuItem.Enabled = false;

            undoClass.setUndoRedoChecked(this);
            LinesGotoActiverAndDeactiver();
        }
        private void wordwrapActiverAndDeactiver()
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                statusToolStripMenuItem.Enabled = false;
                if (StatusBar1.Visible)
                {
                    StatusBar1.Visible = false;
                    statusToolStripMenuItem.Checked = false;
                }
                gotoToolStripMenuItem.Enabled = false;
            }
            else
            {
                statusToolStripMenuItem.Enabled = true;
                if (statusbarVisible == true)
                {
                    StatusBar1.Visible = true;
                    statusToolStripMenuItem.Checked = true;
                }
                gotoToolStripMenuItem.Enabled = true;
                LinesGotoActiverAndDeactiver();
            }
        }
        public void openFile(string path)
        {
            fn = path;
            setTitle();
            txtgeneral.Text = System.IO.File.ReadAllText(fn,returnFileEncoding(fn));
            setEncodingMenuCheckedFromEncodings(returnFileEncoding(fn));
            undoClass.startFromBeginning(txtgeneral, this);
            textChangedUndoSetText = false;
            SaveFlag = true;
            txtgeneral.SelectionStart = 0;
            txtgeneral.SelectionLength = 0;

            if (txtgeneral.ReadOnly)
            {
                txtgeneral.ReadOnly = false;
                readOnlyToolStripMenuItem_Click(null, null);
            }
        }
        //Mostaghim estefade nashavad chun etelaate file ghabl az bein miravad.

        public Frmmain()
        {
            InitializeComponent();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!statusToolStripMenuItem.Checked) statusbarVisible = false;
            else statusbarVisible = true;
            StatusBar1.Visible = statusToolStripMenuItem.Checked;
        }

        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = topMostToolStripMenuItem.Checked;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.AllowScriptChange = false;
            fontDialog1.Font = txtgeneral.Font;
            fontDialog1.ShowDialog();
            txtgeneral.Font = fontDialog1.Font;
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.Color = txtgeneral.BackColor;
            colorDialog1.ShowDialog();
            txtgeneral.BackColor = colorDialog1.Color;
        }

        private void foreColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog2.FullOpen = true;
            colorDialog2.Color = txtgeneral.ForeColor;
            colorDialog2.ShowDialog();
            txtgeneral.ForeColor = colorDialog2.Color;
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtgeneral.WordWrap = wordWrapToolStripMenuItem.Checked;
            wordwrapActiverAndDeactiver();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strForSave;
            if (txtgeneral.ReadOnly)
                strForSave = originalStr;
            else
                strForSave = txtgeneral.Text;


            if (fn == null)
            {
                DialogResult x;
                saveFileDialog1.InitialDirectory = NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath;
                saveFileDialog1.FileName = "";
                x = saveFileDialog1.ShowDialog();
                if (x==DialogResult.OK)
                {
                    fn = saveFileDialog1.FileName;
                    System.IO.File.WriteAllText(fn, strForSave,sendEncodingFromMenu());
                    SaveFlag = true;
                    setTitle();
                    StoreSaveAndOpenDialogSettings();
                }
            }
            else if (fn != null)
            {
                System.IO.File.WriteAllText(fn, strForSave,sendEncodingFromMenu());
                SaveFlag = true;
            }
        }

        private void Frmmain_Load(object sender, EventArgs e)
        {
            logsPath = logsDirectory + logsFileName;
            if (NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath == "")
                NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ZeroTxtgeneralSelection();
            setTitle();
            SaveFlag = true;
            undoClass.startFromBeginning(txtgeneral,this);
            textChangedUndoSetText = false;
            txtmainActiverAndDeactiver();
            pageSetupDialog1.Document = printDocument1;
            loadSettings();
            Frmmain_SizeChanged(null, null);
            setLogin();
        }

        private void txtgeneral_TextChanged(object sender, EventArgs e)
        {
            //Hatman TxtmainDeactiver Bad az False Shodane Aghabgard Bashad.
            //chun Tabee setRedoUndoChecked Az Dade an mikhahad estefade konad.
            undoClass.aghabgardFalser();
            txtmainActiverAndDeactiver();
            if (SaveFlag) SaveFlag = false;
            setStatusBarRowColumn();
            if (textChangedUndoSetText)
            {
                undoClass.setText(txtgeneral,this);
                undoClass.aghabgardTruer();
                textChangedUndoSetText = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveFlag)
            {
                DialogResult a;
                a = MessageBox.Show("Do you want to save changes?", "Notepad#", MessageBoxButtons.YesNoCancel);
                if (a == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                    if (!SaveFlag) return;
                }
                if (a == DialogResult.Cancel) return;
            }
            newToolStripMenuItem_Click_Flag = true;
            if (((ToolStripMenuItem)sender).Equals(newToolStripMenuItem))
            {
                fn = null;
                setTitle();
                txtgeneral.Text = "";
                SaveFlag = true;
                undoClass.startFromBeginning(txtgeneral,this);
                encodingsCheckedFunction(uTF8ToolStripMenuItem, null);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click_Flag = false;
            newToolStripMenuItem_Click((ToolStripMenuItem)sender, null);
            if (newToolStripMenuItem_Click_Flag)
            {
                DialogResult x;
                openFileDialog1.InitialDirectory = NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath;
                openFileDialog1.FileName = "";
                x = openFileDialog1.ShowDialog();
                if (x==DialogResult.OK)
                {
                    openFile(openFileDialog1.FileName);
                    StoreSaveAndOpenDialogSettings();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frmmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ToolStripMenuItem temp = new ToolStripMenuItem();
            newToolStripMenuItem_Click_Flag = false;
            newToolStripMenuItem_Click(temp, null);
            if (!newToolStripMenuItem_Click_Flag) e.Cancel = true;
            if (!e.Cancel)
            {
                saveSettings();
                setLogout();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strForSave;
            if (txtgeneral.ReadOnly)
                strForSave = originalStr;
            else
                strForSave = txtgeneral.Text;

            DialogResult x;
            openFileDialog1.InitialDirectory = NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath;
            if (sendTitle() != null) saveFileDialog1.FileName = sendTitle();
            else saveFileDialog1.FileName = "";
            x = saveFileDialog1.ShowDialog();
            if (x==DialogResult.OK)
            {
                fn = saveFileDialog1.FileName;
                setTitle();
                System.IO.File.WriteAllText(fn, strForSave,sendEncodingFromMenu());
                SaveFlag = true;
                StoreSaveAndOpenDialogSettings();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.SelectionLength != 0) Clipboard.SetText(txtgeneral.SelectedText);
            else
            {
                if(txtgeneral.Text=="") Clipboard.SetText(Environment.NewLine);
                else
                {
                    int currentLine = txtgeneral.GetLineFromCharIndex(txtgeneral.SelectionStart);
                    string clipText = txtgeneral.Lines[currentLine];
                    Clipboard.SetText(clipText + Environment.NewLine);
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                undoClass.setText(txtgeneral, this);
                txtgeneral.SelectedText = Clipboard.GetText();
                undoClass.setText(txtgeneral, this);
                undoClass.aghabgardTruer();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.SelectionLength > 0)
            {
                undoClass.setText(txtgeneral, this);
                txtgeneral.SelectedText = "";
                undoClass.setText(txtgeneral, this);
                undoClass.aghabgardTruer();
            }

            else if (txtgeneral.SelectionStart != txtgeneral.TextLength)
            {
                txtgeneral.SelectionStart++;
                SendKeys.Send("{BS}");
                txtgeneral.Focus();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.SelectionLength!=0)
            {
                copyToolStripMenuItem_Click(null, null);
                deleteToolStripMenuItem_Click(null, null);
            }
            else
            {
                copyToolStripMenuItem_Click(null, null);
                if (txtgeneral.Text != "")
                {
                    int currentLine = txtgeneral.GetLineFromCharIndex(txtgeneral.SelectionStart);
                    int lastLine = txtgeneral.GetLineFromCharIndex(txtgeneral.Text.Length);
                    string remStr = txtgeneral.Lines[currentLine];
                    int remLength = remStr.Length;
                    Boolean remHaveChar = isHaveCharacter(remStr);

                    if (remHaveChar)
                    {
                        undoClass.setText(txtgeneral, this);
                    }

                    string firstStr;
                    if (currentLine == 0) firstStr = "";
                    else
                    {
                        firstStr = txtgeneral.Text.Substring(0, txtgeneral.GetFirstCharIndexOfCurrentLine()-2);
                    }

                    if (remHaveChar)
                        undoClass.setText(txtgeneral, this);

                    if (currentLine == lastLine)
                    {
                        if (currentLine != 0) firstStr += Environment.NewLine;
                        txtgeneral.Text = firstStr;
                        txtgeneral.SelectionStart = txtgeneral.TextLength;                     
                    }
                    else
                    {
                        int secondStrStart = txtgeneral.GetFirstCharIndexOfCurrentLine() + remLength + 2;
                        string secondStr = txtgeneral.Text.Substring(secondStrStart);
                        if (firstStr != "")
                        {
                            txtgeneral.Text = firstStr + Environment.NewLine + secondStr;
                            txtgeneral.SelectionStart = firstStr.Length + 2;
                        }
                        else
                        {
                            txtgeneral.Text = secondStr;
                            txtgeneral.SelectionStart = 0;
                        }
                    }

                    if (remHaveChar)
                    {
                        undoClass.setText(txtgeneral, this);
                        undoClass.aghabgardTruer();
                    }

                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtgeneral.SelectAll();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*frmrep2 findform = new frmrep2(); 
            findform.Show(this);*/
            Frmfind findform = new Frmfind(this);
            findform.Show(this);
        }
        public Boolean generalFind(string s, Boolean findnext, Boolean down, StringComparison CaseSens,Boolean showMessage=true)
        {
            int i;
            if (down)
            {
                if (findnext)
                {
                    i = txtgeneral.Text.IndexOf(s, txtgeneral.SelectionStart + txtgeneral.SelectionLength, CaseSens);
                }
                else
                {
                    i = txtgeneral.Text.IndexOf(s, CaseSens);
                }
            }
            else
            {
                if (findnext)
                {
                    if (txtgeneral.SelectionStart != 0)
                        i = txtgeneral.Text.LastIndexOf(s, txtgeneral.SelectionStart - 1, CaseSens);
                    else i = -1;
                }
                else
                {
                    i = txtgeneral.Text.LastIndexOf(s, CaseSens);
                }
            }
            if (i != -1)
            {
                txtgeneral.SelectionStart = i;
                txtgeneral.SelectionLength = s.Length;
                txtgeneral.ScrollToCaret();
                txtgeneral.Focus();
            }
            if (showMessage)
                if (i == -1)
                {
                    MessageBox.Show("Cannot find " + "\"" + s + "\"", "Notepad#");
                    txtgeneral.Focus();
                }
            if (i != -1) return true;
            else return false;
        }
        public void setSearchVariablesFindForm(string s,Boolean d,StringComparison sc)
        {
            searchString = s;
            searchDown = d;
            searchMatchCase = sc;
        }
        public void setSearchVariablesReplaceForm(string s,string replace,StringComparison sc)
        {
            searchString = s;
            replaceString = replace;
            searchMatchCase = sc;
            searchDown = true;
        }

        private void fIndNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchMatchCase == null || searchDown == null || searchString == null)
                findToolStripMenuItem_Click(null, null);
            else
                generalFind(searchString, true, searchDown, searchMatchCase);
        }

        private void txtgeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 13)
            {
                textChangedUndoSetText = false;
                if (undoClass.sendAlloUndoSetText())
                {
                    undoClass.setText(txtgeneral,this);
                    if (txtgeneral.SelectionLength > 0 && Convert.ToInt32(e.KeyChar) == 8)
                        textChangedUndoSetText = true;
                    if (txtgeneral.SelectionLength==0) undoClass.noterAllow();
                }
                else if (txtgeneral.SelectionLength > 0)
                {
                    undoClass.setText(txtgeneral,this);
                    if(Convert.ToInt32(e.KeyChar) == 8) textChangedUndoSetText = true;
                }

            }
            else if (Convert.ToInt32(e.KeyChar) == 32)
            {
                if (undoClass.sendAlloUndoSetText())
                {
                    undoClass.ppNumberOfSpaces();
                    if (undoClass.sendNumberOfSpaces() == 5)
                    {
                        undoClass.setText(txtgeneral,this);
                        undoClass.zeroForNumberOfSpaces();
                        if (txtgeneral.SelectionLength == 0) undoClass.noterAllow();
                    }
                    else if (txtgeneral.SelectionLength > 0)
                        undoClass.setText(txtgeneral,this);
                }
                else if (txtgeneral.SelectionLength > 0)
                    undoClass.setText(txtgeneral,this);
            }
            else
            {
                if (txtgeneral.SelectionLength > 0)
                    undoClass.setText(txtgeneral,this);
                if (!undoClass.sendAlloUndoSetText()) undoClass.noterAllow();
            }
        }

        private void txtgeneral_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (undoClass.sendAlloUndoSetText())
                {
                    undoClass.setText(txtgeneral,this);
                    if (txtgeneral.SelectionLength == 0) undoClass.noterAllow();
                }
                else if (txtgeneral.SelectionLength > 0)
                {
                    undoClass.setText(txtgeneral,this);
                }
                int temp = txtgeneral.SelectionStart;
                txtgeneral.SelectedText = "\t";
                txtgeneral.SelectionStart = temp + 1;
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (deleteToolStripMenuItem.Checked)
                    deleteToolStripMenuItem_Click(null, null);
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                setStatusBarRowColumn();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoClass.sendAllowUndo()) undoClass.Undo(txtgeneral,this);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoClass.sendAllowRedo()) undoClass.Redo(txtgeneral,this);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmreplace replaceform = new frmreplace(this);
            replaceform.Show(this);
        }
        public void txtgeneralSelectedTextReplace(string find,string replace,StringComparison sc)
        {
            if (txtgeneral.SelectedText.Equals(find, sc))
            {
                int st;
                st = txtgeneral.SelectionStart;
                txtgeneral.SelectedText = replace;
                txtgeneral.SelectionStart = st;
                txtgeneral.SelectionLength = replace.Length;
            }
        }

        private void txtgeneral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                setStatusBarRowColumn();

            if (e.Control && e.KeyCode == Keys.ShiftKey)
            {
                e.Handled = true;
            }

            if (e.Control && (e.KeyCode == Keys.Add || e.KeyCode==Keys.Subtract))
            {
                e.SuppressKeyPress = true;
                Font f = txtgeneral.Font;
                float fontSize = f.Size;
                if (e.KeyCode == Keys.Add && fontSize<=1000) fontSize++;
                else if(fontSize>=2) fontSize--;
                txtgeneral.Font = new Font(f.Name, fontSize, f.Style);
            }

            if (e.Shift && (e.KeyCode == Keys.Enter))
            {
                for(int i=0; 4> i; i++) SendKeys.Send(" ");
            }
        }

        private void setStatusBarRowColumn()
        {
            int row = txtgeneral.GetLineFromCharIndex(txtgeneral.SelectionStart) + 1;
            int col = txtgeneral.SelectionStart - txtgeneral.GetFirstCharIndexFromLine(row - 1) + 1;
            lblrowcol.Text = "Ln " + row.ToString() + ", Col " + col.ToString();
        }

        private void txtgeneral_Click(object sender, EventArgs e)
        {
            setStatusBarRowColumn();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            txtmainActiverAndDeactiver();
        }
        private void saveSettings()
        {
            NotepadSharp.Properties.Settings.Default.BackColor = txtgeneral.BackColor.ToArgb().ToString();
            NotepadSharp.Properties.Settings.Default.ForeColor = txtgeneral.ForeColor.ToArgb().ToString();
            NotepadSharp.Properties.Settings.Default.FontName = txtgeneral.Font.Name;
            NotepadSharp.Properties.Settings.Default.FontSize = txtgeneral.Font.Size;
            NotepadSharp.Properties.Settings.Default.FontStyle = txtgeneral.Font.Style.ToString();
            NotepadSharp.Properties.Settings.Default.FormHeight = mainFormHeight;
            NotepadSharp.Properties.Settings.Default.FormWidth = mainFormWidth;
            NotepadSharp.Properties.Settings.Default.StatusChecked = statusToolStripMenuItem.Checked;
            NotepadSharp.Properties.Settings.Default.topMostChecked = topMostToolStripMenuItem.Checked;
            NotepadSharp.Properties.Settings.Default.WordWrapChecked = wordWrapToolStripMenuItem.Checked;
            NotepadSharp.Properties.Settings.Default.PageSetupMarginLeft = pageSetupDialog1.PageSettings.Margins.Left;
            NotepadSharp.Properties.Settings.Default.PageSetupMarginTop = pageSetupDialog1.PageSettings.Margins.Top;
            NotepadSharp.Properties.Settings.Default.PageSetupMarginRight = pageSetupDialog1.PageSettings.Margins.Right;
            NotepadSharp.Properties.Settings.Default.PageSetupMarginBottom = pageSetupDialog1.PageSettings.Margins.Bottom;
            NotepadSharp.Properties.Settings.Default.Save();
        }
        private void loadSettings()
        {
            txtgeneral.BackColor = Color.FromArgb(Convert.ToInt32(NotepadSharp.Properties.Settings.Default.BackColor));
            txtgeneral.ForeColor = Color.FromArgb(Convert.ToInt32(NotepadSharp.Properties.Settings.Default.ForeColor));
            Font f = new Font(NotepadSharp.Properties.Settings.Default.FontName, NotepadSharp.Properties.Settings.Default.FontSize);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Bold"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Bold);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Italic"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Italic);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Underline"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Underline);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Strikeout"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Strikeout);
            txtgeneral.Font = f;

            this.Height = NotepadSharp.Properties.Settings.Default.FormHeight;
            this.Width = NotepadSharp.Properties.Settings.Default.FormWidth;

            if (NotepadSharp.Properties.Settings.Default.StatusChecked)
            {
                statusbarVisible = true;
                StatusBar1.Visible = true;
                statusToolStripMenuItem.Checked = true;
            }
            else
            {
                statusbarVisible = false;
                StatusBar1.Visible = false;
                statusToolStripMenuItem.Checked = false;
            }

            topMostToolStripMenuItem.Checked = NotepadSharp.Properties.Settings.Default.topMostChecked;
            topMostToolStripMenuItem_Click(null, null);

            wordWrapToolStripMenuItem.Checked = NotepadSharp.Properties.Settings.Default.WordWrapChecked;
            wordwrapActiverAndDeactiver();
            pageSetupDialog1.PageSettings.Margins.Left = NotepadSharp.Properties.Settings.Default.PageSetupMarginLeft;
            pageSetupDialog1.PageSettings.Margins.Top = NotepadSharp.Properties.Settings.Default.PageSetupMarginTop;
            pageSetupDialog1.PageSettings.Margins.Right = NotepadSharp.Properties.Settings.Default.PageSetupMarginRight;
            pageSetupDialog1.PageSettings.Margins.Bottom = NotepadSharp.Properties.Settings.Default.PageSetupMarginBottom;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult x;
            printDialog1.Document = printDocument1;
            pageSetupDialog1.Document = printDocument1;
            x = printDialog1.ShowDialog();
            if (x == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txtgeneral.Text, txtgeneral.Font, Brushes.Black,0,0);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fn != null)
            {
                DialogResult x;
                x = MessageBox.Show("Changes may lost. Are you sure?", "Notepad#", MessageBoxButtons.OKCancel);
                if (x == DialogResult.OK)
                {
                    openFile(fn);
                }
            }
            else
                MessageBox.Show("This file does not exist on disk!", "Notepad#");
        }

        private void backToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtgeneral.Font = new Font("Consolas", 11);
            txtgeneral.BackColor = Color.FromArgb(-1);
            txtgeneral.ForeColor = Color.FromArgb(-16777216);
            saveSettings();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmgoto frmGotoLine = new frmgoto(this);
            frmGotoLine.ShowDialog(this);
        }

        private void Frmmain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                mainFormHeight = this.Height;
                mainFormWidth = this.Width;
            }
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoClass.setText(txtgeneral, this);
            txtgeneral.SelectedText = sendCurrentTimeAndDate();
        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmabout aboutform = new frmabout();
            aboutform.ShowDialog(this);
        }

        private void Frmmain_DragDrop(object sender, DragEventArgs e)
        {
            newToolStripMenuItem_Click_Flag = false;
            ToolStripMenuItem temp = new ToolStripMenuItem();
            newToolStripMenuItem_Click(temp, null);
            if (newToolStripMenuItem_Click_Flag)
            {
                // Extract the data from the DataObject-Container into a string list
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                // Do something with the data...
                openFile(FileList[0]);
            }
        }

        private void Frmmain_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        public void GoToLine(int number)
        {
            //Dar Forme Goto Check Shode Number Bozorg Tar Az Tedade Line Ha Nabashad.
            txtgeneral.SelectionStart = txtgeneral.GetFirstCharIndexFromLine(number);
            txtgeneral.ScrollToCaret();
        }
        private void setLogin()
        {
            LogIn = "[" + sendCurrentTimeAndDate() + "] --> ";
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.RightToLeft == RightToLeft.No)
                txtgeneral.RightToLeft = RightToLeft.Yes;
            else
                txtgeneral.RightToLeft = RightToLeft.No;
        }

        private void encodingsCheckedFunction(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem Menu in encodingToolStripMenuItem.DropDownItems)
            {
                if (sender.Equals(Menu))
                    Menu.Checked = true;
                else
                    Menu.Checked = false;
            }
        }

        private void txtgeneral_RightToLeftChanged(object sender, EventArgs e)
        {
            if (txtgeneral.RightToLeft == RightToLeft.Yes)
                rightToLeftToolStripMenuItem.Checked = true;
            else
                rightToLeftToolStripMenuItem.Checked = false;

        }

        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtgeneral.ReadOnly = readOnlyToolStripMenuItem.Checked;

            Boolean tempFlag = SaveFlag;
            if (txtgeneral.ReadOnly)
            {
                originalStr = txtgeneral.Text;
                StringBuilder generatedStr = new StringBuilder(originalStr);
                for (int i = 0; generatedStr.Length > i; i++)
                {
                    if (generatedStr[i] == '\n' && i != 0 && generatedStr[i - 1] != '\r')
                    {
                        generatedStr.Remove(i, 1);
                        generatedStr.Insert(i, "\r\n");
                    }
                }
                txtgeneral.Text = generatedStr.ToString();
            }
            else
            {
                txtgeneral.Text = originalStr;
                originalStr = "";
            }
            SaveFlag = tempFlag;
        }


        private void setLogout()
        {
            if (System.IO.File.Exists(logsPath))
                System.IO.File.AppendAllText(logsPath, Environment.NewLine + LogIn + "[" + sendCurrentTimeAndDate() + "]");
            else
            {
                if (!System.IO.Directory.Exists(logsDirectory))
                    System.IO.Directory.CreateDirectory(logsDirectory);
                System.IO.File.WriteAllText(logsPath, LogIn + "[" + sendCurrentTimeAndDate() + "]");
            }
        }
    }
    class myUndo
    {
        Boolean allowUndoSetText;
        //zamani ke false bashad settext farakhani nakhahad shod.
        int numberOfSpaces;
        string[] temp = new string[350];
        int[] tempSelectionStart = new int[350];
        public int[] tempSelectionLength = new int[350];
        int currentposition;
        int index;
        Boolean aghabgard;
        // Hargah textchanged etefagh oftad aghabgard false mishavad. MOHEM!!!
        public myUndo()
        {
            currentposition = 0;
            index = 0;
            numberOfSpaces = 0;
            allowUndoSetText = true;
            aghabgard = true;
        }
        public void startFromBeginning(TextBox tb,Frmmain frm)
        {
            temp[0] = tb.Text;
            tempSelectionStart[0] = tb.SelectionStart;
            tempSelectionLength[0] = tb.SelectionLength;
            currentposition = 0;
            index = 0;
            numberOfSpaces = 0;
            allowUndoSetText = true;
            aghabgard = true;
            setUndoRedoChecked(frm);
        }
        public void aghabgardFalser() { if (aghabgard) aghabgard = false; }
        public void aghabgardTruer() { if (!aghabgard) aghabgard = true; }
        public int sendNumberOfSpaces() { return numberOfSpaces; }
        public Boolean sendAlloUndoSetText() { return allowUndoSetText; }
        public void zeroForNumberOfSpaces() { numberOfSpaces = 0; }
        public void noterAllow() { allowUndoSetText = !allowUndoSetText; }
        public void ppNumberOfSpaces() { numberOfSpaces++; }
        public Boolean sendAllowUndo()
        {
            if (currentposition > 0 || (!aghabgard && currentposition >= 0)) return true;
            return false;
        }
        public Boolean sendAllowRedo()
        {
            if (index > currentposition) return true;
            return false;
        }
        public void setUndoRedoChecked(Frmmain frm)
        {
            if (sendAllowUndo()) frm.setUndoChecked(true);
            else frm.setUndoChecked(false);
            if (sendAllowRedo()) frm.setRedoChecked(true);
            else frm.setRedoChecked(false);
        }
        //agar tabe e settext farakhani shavad darhali ke pas az an textchanged etefagh nayoftad
        //bayad be surate dasti aghabgard true shavad.
        public void setText(TextBox tb,Frmmain frm)
        {
            if (tb.Text != temp[currentposition])
            {
                if (currentposition == 349)
                {
                    for (int i = 0; 250 > i; i++)
                    {
                        temp[i] = temp[i + 100];
                        tempSelectionStart[i] = tempSelectionStart[i + 100];
                        tempSelectionLength[i] = tempSelectionLength[i + 100];
                    }
                    currentposition = 249;
                    index = 249;
                }
                index = currentposition + 1;
                temp[index] = tb.Text;
                tempSelectionStart[index] = tb.SelectionStart;
                tempSelectionLength[index] = tb.SelectionLength;
                currentposition++;
            }
            else if(tb.SelectionStart != tempSelectionStart[currentposition])
            {
                index = currentposition;
                tempSelectionStart[index] = tb.SelectionStart;
                tempSelectionLength[index] = tb.SelectionLength;
            }
            setUndoRedoChecked(frm);
        }
        public void Undo(TextBox tb, Frmmain frm)
        {
            if(currentposition>0 || (!aghabgard && currentposition>=0)){
                if (!aghabgard)
                {
                    if (index - currentposition <= 5)
                    {
                        setText(tb, frm);
                        currentposition--;
                    }
                    tb.Text = temp[currentposition];
                    tb.SelectionStart = tempSelectionStart[currentposition];
                    tb.SelectionLength = tempSelectionLength[currentposition];
                    tb.ScrollToCaret();
                    aghabgard = true;
                }
                else
                {
                    tb.Text= temp[--currentposition];
                    tb.SelectionStart = tempSelectionStart[currentposition];
                    tb.SelectionLength = tempSelectionLength[currentposition];
                    tb.ScrollToCaret();
                    aghabgard = true;
                }
                setUndoRedoChecked(frm);
            }
        }
        public void Redo(TextBox tb,Frmmain frm)
        {
            if (index > currentposition)
            {
                tb.Text= temp[++currentposition];
                tb.SelectionStart = tempSelectionStart[currentposition];
                tb.SelectionLength = tempSelectionLength[currentposition];
                tb.ScrollToCaret();
                aghabgard = true;
            }
            setUndoRedoChecked(frm);
        }
    }
}