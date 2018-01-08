﻿using System;
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
        string logsPath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        myUndo undoClass = new myUndo();
        string fn;
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
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                findToolStripMenuItem.Enabled = false;
                fIndNextToolStripMenuItem.Enabled = false;
            }
            else
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                findToolStripMenuItem.Enabled = true;
                fIndNextToolStripMenuItem.Enabled = true;
            }
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
            txtgeneral.Text = System.IO.File.ReadAllText(fn);
            undoClass.startFromBeginning(txtgeneral, this);
            textChangedUndoSetText = false;
            SaveFlag = true;
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
            if (fn == null)
            {
                DialogResult x;
                saveFileDialog1.InitialDirectory = NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath;
                saveFileDialog1.FileName = "";
                x = saveFileDialog1.ShowDialog();
                if (x==DialogResult.OK)
                {
                    fn = saveFileDialog1.FileName;
                    SaveFlag = true;
                    setTitle();
                    StoreSaveAndOpenDialogSettings();
                }
            }
            if (fn != null)
            {
                System.IO.File.WriteAllText(fn, txtgeneral.Text);
                SaveFlag = true;
            }
        }

        private void Frmmain_Load(object sender, EventArgs e)
        {
            if (NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath == "")
                NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ZeroTxtgeneralSelection();
            fn = null;
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
            DialogResult x;
            openFileDialog1.InitialDirectory = NotepadSharp.Properties.Settings.Default.SaveAndOpenDialogPath;
            if (sendTitle() != null) saveFileDialog1.FileName = sendTitle();
            else saveFileDialog1.FileName = "";
            x = saveFileDialog1.ShowDialog();
            if (x==DialogResult.OK)
            {
                fn = saveFileDialog1.FileName;
                setTitle();
                System.IO.File.WriteAllText(fn, txtgeneral.Text);
                SaveFlag = true;
                StoreSaveAndOpenDialogSettings();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.SelectionLength != 0) Clipboard.SetText(txtgeneral.SelectedText);
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
            undoClass.setText(txtgeneral,this);
            txtgeneral.SelectedText = "";
            undoClass.setText(txtgeneral,this);
            undoClass.aghabgardTruer();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtgeneral.SelectionLength!=0)
            {
                copyToolStripMenuItem_Click(null, null);
                deleteToolStripMenuItem_Click(null, null);
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
                txtgeneral.Focus();
            }
            if (showMessage) if(i==-1) MessageBox.Show("Cannot find " + "\"" + s + "\"", "Notepad#");
            txtgeneral.Focus();
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
            txtgeneral_KeyDown(null, e);
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
           if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                setStatusBarRowColumn();
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
        private void selectionActiverAndDeactiver()
        {
            if (txtgeneral.SelectionLength > 0)
            {
                copyToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            selectionActiverAndDeactiver();
            if (Clipboard.ContainsText()) pasteToolStripMenuItem.Enabled = true;
            else pasteToolStripMenuItem.Enabled = false;
        }
        private void saveSettings()
        {
            /*string[] settings = new string[13];
            settings[0]= txtgeneral.BackColor.ToArgb().ToString();
            settings[1] = txtgeneral.ForeColor.ToArgb().ToString();
            settings[2] = txtgeneral.Font.Name;
            settings[3] = Convert.ToInt16(txtgeneral.Font.Size).ToString();
            settings[4] = txtgeneral.Font.Style.ToString();
            settings[5] = mainFormHeight.ToString();
            settings[6] = mainFormWidth.ToString();
            settings[7] = statusToolStripMenuItem.Checked.ToString();
            settings[8] = wordWrapToolStripMenuItem.Checked.ToString();
            settings[9] = pageSetupDialog1.PageSettings.Margins.Left.ToString();
            settings[10] = pageSetupDialog1.PageSettings.Margins.Top.ToString();
            settings[11] = pageSetupDialog1.PageSettings.Margins.Right.ToString();
            settings[12] = pageSetupDialog1.PageSettings.Margins.Bottom.ToString();
            System.IO.File.WriteAllLines(settingsPath, settings);*/


            NotepadSharp.Properties.Settings.Default.BackColor = txtgeneral.BackColor.ToArgb().ToString();
            NotepadSharp.Properties.Settings.Default.ForeColor = txtgeneral.ForeColor.ToArgb().ToString();
            NotepadSharp.Properties.Settings.Default.FontName = txtgeneral.Font.Name;
            NotepadSharp.Properties.Settings.Default.FontSize = Convert.ToInt16(txtgeneral.Font.Size).ToString();
            NotepadSharp.Properties.Settings.Default.FontStyle = txtgeneral.Font.Style.ToString();
            NotepadSharp.Properties.Settings.Default.FormHeight = mainFormHeight.ToString();
            NotepadSharp.Properties.Settings.Default.FormWidth = mainFormWidth.ToString();
            NotepadSharp.Properties.Settings.Default.StatusChecked = statusToolStripMenuItem.Checked.ToString();
            NotepadSharp.Properties.Settings.Default.WordWrapChecked = wordWrapToolStripMenuItem.Checked.ToString();
            NotepadSharp.Properties.Settings.Default.PageSetupMarginLeft = pageSetupDialog1.PageSettings.Margins.Left.ToString();
            NotepadSharp.Properties.Settings.Default.PageSetupMarginTop = pageSetupDialog1.PageSettings.Margins.Top.ToString();
            NotepadSharp.Properties.Settings.Default.PageSetupMarginRight = pageSetupDialog1.PageSettings.Margins.Right.ToString();
            NotepadSharp.Properties.Settings.Default.PageSetupMarginBottom = pageSetupDialog1.PageSettings.Margins.Bottom.ToString();
            NotepadSharp.Properties.Settings.Default.Save();
        }
        private void loadSettings()
        {
            /*if (System.IO.File.Exists(settingsPath))
            {
                string[] settings = new string[12];
                settings = System.IO.File.ReadAllLines(settingsPath);
                txtgeneral.BackColor = Color.FromArgb(Convert.ToInt32(settings[0]));
                txtgeneral.ForeColor = Color.FromArgb(Convert.ToInt32(settings[1]));
                Font f = new Font(settings[2],Convert.ToInt16(settings[3]));
                if (settings[4].Contains("Bold"))
                    f = new Font(f.Name, f.Size,f.Style | FontStyle.Bold);
                if (settings[4].Contains("Italic"))
                    f = new Font(f.Name, f.Size, f.Style | FontStyle.Italic);
                if (settings[4].Contains("Underline"))
                    f = new Font(f.Name, f.Size, f.Style | FontStyle.Underline);
                if (settings[4].Contains("Strikeout"))
                    f = new Font(f.Name, f.Size, f.Style | FontStyle.Strikeout);
                txtgeneral.Font = f;
                this.Height = Convert.ToInt32(settings[5]);
                this.Width = Convert.ToInt32(settings[6]);
                if (Convert.ToBoolean(settings[7]))
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
                wordWrapToolStripMenuItem.Checked = Convert.ToBoolean(settings[8]);
                wordwrapActiverAndDeactiver();
                pageSetupDialog1.PageSettings.Margins.Left = Convert.ToInt32(settings[9]);
                pageSetupDialog1.PageSettings.Margins.Top = Convert.ToInt32(settings[10]);
                pageSetupDialog1.PageSettings.Margins.Right = Convert.ToInt32(settings[11]);
                pageSetupDialog1.PageSettings.Margins.Bottom = Convert.ToInt32(settings[12]);
            }*/

            txtgeneral.BackColor = Color.FromArgb(Convert.ToInt32(NotepadSharp.Properties.Settings.Default.BackColor));
            txtgeneral.ForeColor = Color.FromArgb(Convert.ToInt32(NotepadSharp.Properties.Settings.Default.ForeColor));
            Font f = new Font(NotepadSharp.Properties.Settings.Default.FontName, Convert.ToInt16(NotepadSharp.Properties.Settings.Default.FontSize));
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Bold"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Bold);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Italic"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Italic);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Underline"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Underline);
            if (NotepadSharp.Properties.Settings.Default.FontStyle.Contains("Strikeout"))
                f = new Font(f.Name, f.Size, f.Style | FontStyle.Strikeout);
            txtgeneral.Font = f;
            this.Height = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.FormHeight);
            this.Width = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.FormWidth);
            if (Convert.ToBoolean(NotepadSharp.Properties.Settings.Default.StatusChecked))
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
            wordWrapToolStripMenuItem.Checked = Convert.ToBoolean(NotepadSharp.Properties.Settings.Default.WordWrapChecked);
            wordwrapActiverAndDeactiver();
            pageSetupDialog1.PageSettings.Margins.Left = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.PageSetupMarginLeft);
            pageSetupDialog1.PageSettings.Margins.Top = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.PageSetupMarginTop);
            pageSetupDialog1.PageSettings.Margins.Right = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.PageSetupMarginRight);
            pageSetupDialog1.PageSettings.Margins.Bottom = Convert.ToInt32(NotepadSharp.Properties.Settings.Default.PageSetupMarginBottom);
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
            undoClass.setText(txtgeneral, this);
            txtgeneral.Text = System.IO.File.ReadAllText(fn);
            undoClass.setText(txtgeneral, this);
            undoClass.aghabgardTruer();
            SaveFlag = true;
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
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Do something with the data...
            txtgeneral.Text = System.IO.File.ReadAllText(FileList[0]);
            fn = FileList[0];
            setTitle();
            SaveFlag = true;
            undoClass.startFromBeginning(txtgeneral, this);
            textChangedUndoSetText = false;
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
            if (System.IO.File.Exists(logsPath))
                System.IO.File.AppendAllText(logsPath, Environment.NewLine + "[" + sendCurrentTimeAndDate() + "] --> ");
            else
                System.IO.File.WriteAllText(logsPath, "[" + sendCurrentTimeAndDate() + "] --> ");
        }

        private void setLogout()
        {
            System.IO.File.AppendAllText(logsPath, "[" + sendCurrentTimeAndDate() + "]");
        }
    }
    class myUndo
    {
        Boolean allowUndoSetText;
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
                setUndoRedoChecked(frm);
            }
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
                aghabgard = true;
            }
            setUndoRedoChecked(frm);
        }
    }
}