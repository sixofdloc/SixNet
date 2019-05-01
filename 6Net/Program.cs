using System;
using System.Threading;
using Net_Data;
using Net_Logger;

namespace Net
{
    class MainClass
    {
        private static bool quitFlag = false;
        public static void Main(string[] args)
        {

            Console.Clear();
            Console.WriteLine("SixNet BBS, Starting up...");
            var bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig("./");
            if (bbsDatabaseConfiguration == null)
            {
                Console.WriteLine("Run setup utility first.");
                return;
            }
            var connectionString = BBSDatabaseConfiguration.BuildConnectionString(bbsDatabaseConfiguration);
            if (!BBSDatabaseConfiguration.IsDatabaseSetup(connectionString))
            {
                Console.WriteLine("Database not properly configured, run setup utility.");
                return;
            }
            LoggingAPI.Init("./Logs/");
            LoggingAPI.LogEntry("Software started.");
            try
            {
                quitFlag = false;
                BBSServer bbsServer = new BBSServer(connectionString);
                bbsServer.Start();
                while (!quitFlag)
                {
                    Thread.Sleep(1000);
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Q)
                        {
                            quitFlag = true;
                            bbsServer.Stop();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
            }
            finally
            {
                LoggingAPI.LogEntry("Software shutdown.");
                LoggingAPI.FlushQueue();
            }
        }
    }
}
