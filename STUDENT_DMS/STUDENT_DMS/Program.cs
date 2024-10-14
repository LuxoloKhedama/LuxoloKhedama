using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STUDENT_DMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (SPLASHSCREEN S = new SPLASHSCREEN())
            {
                Application.DoEvents();
                S.Show();
                System.Threading.Thread.Sleep(3000);

            }
            Application.Run(new LOGIN());
        }
    }
}
