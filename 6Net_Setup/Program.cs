using System;
using System.Data;
using System.Linq;
using Net_Data;
using Net_Data.Models;
using Net_Logger;
using Net_Setup.Classes;
using Net_Setup.Classes.Setup;

namespace Net_Setup
{
    partial class MainClass
    {

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("SixNet Setup Utility");
            try
            {
                var basePath = "./";
                var bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig(basePath);
                if (bbsDatabaseConfiguration == null)
                {
                    basePath = Utils.Input("BBS Base Path", basePath);
                    bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig(basePath);
                    if (bbsDatabaseConfiguration == null) bbsDatabaseConfiguration = new BBSDatabaseConfiguration();
                }
                LoggingAPI.Init(basePath);
                new MainMenu(bbsDatabaseConfiguration,basePath).Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
