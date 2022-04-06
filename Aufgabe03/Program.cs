using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFormsProject
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach(string s in args)
            {
                if(s.ToLower().Equals("autoexport"))
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Form1 f1 = new Form1();
                    //Application.Run(f1);
                    f1.Form1_Load(null, null);
                    f1.button3_Click(null, null);
                    Application.Exit();
                }
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch { }
        }
    }
}
