using System;
using System.Threading;
using Net_Data;
using Net_Logger;

namespace Net
{
    class MainClass
    {
        private static BBSDataCore _core;
        private static string _connectionString = "";
        private static bool quitFlag = false;
        private static BBSDatabaseConfiguration _bbsDatabaseConfiguration;

        public static void Main(string[] args)
        {

            Console.Clear();
            Console.WriteLine("SixNet BBS, Starting up...");
            _bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig("./");
            if (_bbsDatabaseConfiguration == null)
            {
                Console.WriteLine("Run setup utility first.");
                return;
            }
            _connectionString = BBSDatabaseConfiguration.BuildConnectionString(_bbsDatabaseConfiguration);
            LoggingAPI.Init("./Logs/");
            if (BBSDatabaseConfiguration.IsDatabaseSetup(_connectionString))
            {
                Console.WriteLine("Database configured.");
                _core = new BBSDataCore(_connectionString);
            }
            else
            {
                Console.WriteLine("Database not configured - run setup utility.");
                return;
            }
            var config = _core.GetBBSConfig();
            LoggingAPI.LogEntry("Software started.");
            try
            {
                quitFlag = false;
                BBSServer bbsServer = new BBSServer(_connectionString,config.BBSPort,"BBS Server");
                bbsServer.Start();
                while (!quitFlag)
                {
                    Thread.Sleep(100);
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
