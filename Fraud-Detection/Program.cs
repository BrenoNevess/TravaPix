using System;
using System.Windows.Forms;
using FraudDetection.Interface.Forms;

namespace FraudDetection
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}