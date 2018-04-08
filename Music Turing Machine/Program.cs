using System;
using System.Windows.Forms;

namespace Music_Turing_Machine
{
    static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgramForm());
        }
    }
}
