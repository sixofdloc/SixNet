using System;
using System.Windows.Forms;
using SixNet;
using SixNet_Logger;

namespace SixNet_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoggingAPI.LogEntry("Software started.");
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception at Main: " + e.Message);
            }
            LoggingAPI.LogEntry("Software shutdown.");
            LoggingAPI.FlushQueue();
        }
    }
}
