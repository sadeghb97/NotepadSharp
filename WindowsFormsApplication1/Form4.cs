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
    public partial class frmgoto : Form
    {
        Frmmain frmgeneral;
        public frmgoto(Frmmain frm)
        {
            frmgeneral = frm;
            InitializeComponent();
        }

        private void txtgotoline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) || e.KeyChar == '.')
            {
                e.Handled = true;
                MessageBox.Show("Unacceptable Character!", "Notepad#");
            }
        }

        private void frmgoto_Load(object sender, EventArgs e)
        {
            txtgotoline.ContextMenu = new ContextMenu();
            txtgotoline.Text = frmgeneral.sendCurrentLine().ToString();
            txtgotoline.SelectAll();
        }

        private void txtgotoline_TextChanged(object sender, EventArgs e)
        {
            if (txtgotoline.Text == "")
                btngoto.Enabled = false;
            else btngoto.Enabled = true;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btngoto_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(txtgotoline.Text);
            if (number > frmgeneral.sendNumberOfLines())
            {
                MessageBox.Show("The line number is beyond the total number of lines", "Notepad# - Goto Line");
                txtgotoline.Text = frmgeneral.sendNumberOfLines().ToString();
                txtgotoline.SelectAll();
            }
            else
            {
                frmgeneral.GoToLine(number - 1);
                this.Close();
            }
        }
    }
}
