using System;
using System.Windows.Forms;

namespace Aplikasi_Sistem_Informasi_Penjara
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
            Application.Run(new LoginF());
        }
    }
}
