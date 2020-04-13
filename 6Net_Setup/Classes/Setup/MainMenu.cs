using System;
using System.Linq;
using Net_Console;
using Net_Data;

namespace Net_Setup.Classes.Setup
{
    public class MainMenu
    {
        private BBSDataCore _core;
        private BBSDatabaseConfiguration _configuration;
        private readonly string _basePath;

        private bool _exitFlag;

        public MainMenu(BBSDatabaseConfiguration configuration, string basePath)
        {
            _basePath = basePath;
            _configuration = configuration;
        }

        public void Menu()
        {
            _exitFlag = false;
            while (!_exitFlag)
            {
                var selection = -1;
                if (_configuration.IsDatabaseSetup())
                {
                    Console.WriteLine("Database configured.");
                    _core = new BBSDataCore(_configuration.ConnectionString());
                    Menu menu = new Menu("Database IS Configured",Banners.SixNet.Concat(Banners.SetupUtility).ToArray(), 
                        new System.Collections.Generic.List<MenuOption> { 
                        new MenuOption(1, "Configure Database"),
                        new MenuOption(2, "Configure BBS"),
                        new MenuOption(3, "Setup Message Bases"),
                        new MenuOption(4, "Setup UD Bases"),
                        new MenuOption(5, "Setup GFile Areas"),
                        new MenuOption(6, "Setup PFile Areas"),
                        new MenuOption(8, "Setup Access Groups"),
                        new MenuOption(8, "Test 1"),
                        new MenuOption(8, "Test 2"),
                        new MenuOption(8, "Test 3"),
                        new MenuOption(8, "Test 4"),
                        new MenuOption(8, "Test 5"),
                        new MenuOption(8, "Test 6"),
                        new MenuOption(8, "Test 7"),
                        new MenuOption(8, "Test 8"),
                        new MenuOption(8, "Test 9"),
                        new MenuOption(8, "Test 0"),
                        new MenuOption(99, "Save Configuration"),
                        new MenuOption(-1, "Exit"),
                    });
                    selection = menu.Go();
                }
                else
                {
                    Menu menu = new Menu("Database IS NOT Configured", Banners.SixNet.Concat(Banners.SetupUtility).ToArray(),
                        new System.Collections.Generic.List<MenuOption> {
                        new MenuOption(1, "Configure Database"),
                        new MenuOption(-1, "Exit"),
                    });
                    selection = menu.Go();
                }
                //Utils.Divider();
                //Console.WriteLine("1. Configure  Database");
                //if (_configuration.IsDatabaseSetup())
                //{
                //    Console.WriteLine("2. Configure BBS");
                //    Console.WriteLine("3. Setup Message Bases");
                //    Console.WriteLine("4. Setup UD Bases");
                //    Console.WriteLine("5. Setup GFile Areas");
                //    Console.WriteLine("6. Setup PFile Areas");
                //    Console.WriteLine("S. Save Configuration");
                //}
                //Console.WriteLine("X. Exit");
                //var selection = Utils.Input("Select", "");
                switch (selection)
                {
                    case 1:
                        new DatabaseSetup(_configuration).SetupDatabase();
                        break;
                    case 2:
                        new BBSSetup(_core).SetupBBS();
                        break;
                    case 3:
                        new MessageBaseSetup(_core).SetupMessageBases();
                        break;
                    case 5:
                        new GFilesSetup(_core).SetupGFileAreas();
                        break;
                    case 6:
                        new PFilesSetup(_core).SetupPFileAreas();
                        break;
                    case 99:
                        BBSDatabaseConfiguration.SaveConfig(_basePath, _configuration);
                        break;
                    case -1:
                        _exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
