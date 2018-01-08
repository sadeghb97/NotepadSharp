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
    public partial class frmreplace : Form
    {
        Frmmain frmgeneral;
        public frmreplace(Frmmain frm)
        {
            frmgeneral = frm;
            InitializeComponent();
        }

        private void frmreplace_Load(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count > 2)
            {
                Application.OpenForms[1].Focus();
                this.Close();
            }
            if (frmgeneral.sendSearchString() != null)
                txtfindwhat.Text = frmgeneral.sendSearchString();
            if (frmgeneral.sendReplaceString() != null)
                txtreplacewith.Text = frmgeneral.sendReplaceString();
            txtfindwhat_TextChanged(null, null);
        }

        private void btnfindnext_Click(object sender, EventArgs e)
        {
            StringComparison sc;
            if (chbmatchcase.Checked) sc = StringComparison.Ordinal;
            else sc = StringComparison.OrdinalIgnoreCase;
            frmgeneral.generalFind(txtfindwhat.Text, true, true, sc);
            frmgeneral.setSearchVariablesReplaceForm(txtfindwhat.Text,txtreplacewith.Text,sc);
        }

        private void txtfindwhat_TextChanged(object sender, EventArgs e)
        {
            if (txtfindwhat.Text == "")
            {
                btnfindnext.Enabled = false;
                btnreplace.Enabled = false;
                btnreplaceAll.Enabled = false;
            }
            else
            {
                btnfindnext.Enabled = true;
                btnreplace.Enabled = true;
                btnreplaceAll.Enabled = true;
            }
        }

        private void btnreplace_Click(object sender, EventArgs e)
        {
            StringComparison sc;
            if (chbmatchcase.Checked) sc = StringComparison.Ordinal;
            else sc = StringComparison.OrdinalIgnoreCase; 
            frmgeneral.txtgeneralSelectedTextReplace(txtfindwhat.Text,txtreplacewith.Text,sc);
            btnfindnext_Click(null, null);
        }

        private void btnreplaceAll_Click(object sender, EventArgs e)
        {
            frmgeneral.ZeroTxtgeneralSelection();
            Boolean resume = true;
            StringComparison sc;
            if (chbmatchcase.Checked) sc = StringComparison.Ordinal;
            else sc = StringComparison.OrdinalIgnoreCase;
            while (resume)
            {
                resume=frmgeneral.generalFind(txtfindwhat.Text, true, true, sc,false);
                if(resume)frmgeneral.txtgeneralSelectedTextReplace(txtfindwhat.Text, txtreplacewith.Text, sc);
            }
            frmgeneral.setSearchVariablesReplaceForm(txtfindwhat.Text, txtreplacewith.Text, sc);
            frmgeneral.ZeroTxtgeneralSelection();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
