using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //with args(user open file with the program)
            if (args != null && args.Length > 0)
            {
                string fileName = args[0];
                //Check file exists
                if (System.IO.File.Exists(fileName))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Frmmain MainFrom = new Frmmain();
                    MainFrom.openFile(fileName);
                    Application.Run(MainFrom);
                }
                //The file does not exist
                else
                {
                    MessageBox.Show("The file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Frmmain());
                }
            }
            //without args
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Frmmain());
            }
        }
    }
}
