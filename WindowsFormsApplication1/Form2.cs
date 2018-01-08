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
    public partial class Frmfind : Form
    {
        protected Frmmain frmgeneral;
        //protected ta agar az in form ers bari kardim betavanim az an estefade konim.
        public Frmfind(Frmmain frm)
        {
            frmgeneral = frm;
            InitializeComponent();
        }
        public Frmfind()
        {
            InitializeComponent();
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            StringComparison sc;
            if (chbmatchcase.Checked) sc = StringComparison.Ordinal;
            else sc = StringComparison.OrdinalIgnoreCase;
            frmgeneral.generalFind(txtfind.Text, false, rdodown.Checked, sc);
            frmgeneral.setSearchVariablesFindForm(txtfind.Text, rdodown.Checked, sc);
        }

        private void btnfindnext_Click(object sender, EventArgs e)
        {
            StringComparison sc;
            if (chbmatchcase.Checked) sc = StringComparison.Ordinal;
            else sc = StringComparison.OrdinalIgnoreCase;
            frmgeneral.generalFind(txtfind.Text, true, rdodown.Checked, sc);
            frmgeneral.setSearchVariablesFindForm(txtfind.Text, rdodown.Checked, sc);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frmfind_Load(object sender, EventArgs e)
        {
            if (this.Site == null || !this.Site.DesignMode)
            {
                // Not in design mode, okay to do dangerous stuff...
                if (Application.OpenForms.Count > 2)
                {
                    Application.OpenForms[1].Focus();
                    this.Close();
                }
                if (frmgeneral.sendSearchString() != null)
                    txtfind.Text = frmgeneral.sendSearchString();
                txtfind_TextChanged(null, null);
            }
        }

        private void txtfind_TextChanged(object sender, EventArgs e)
        {
            if (txtfind.Text == "")
            {
                btnfind.Enabled = false;
                btnfindnext.Enabled = false;
            }
            else
            {
                btnfind.Enabled = true;
                btnfindnext.Enabled = true;
            }
        }
    }
}
