using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Net_Data;
using Net_Data.Models;
using Net_Setup.Classes;

namespace Net_Setup.Classes.Setup
{
    public class GFilesSetup
    {
        private readonly BBSDataCore _core;
        private GFileArea currentGFileArea = null;
        private List<GFileArea> gfileAreas = null;


        public GFilesSetup(BBSDataCore core)
        {
            _core = core;
        }

        private void GFileHeader()
        {
            gfileAreas = _core.GfileAreas();
            Console.Clear();
            Console.WriteLine("Setup GFile Areas");
            Utils.Divider();
            if (currentGFileArea == null )//|| currentArea.ParentAreaId == null)
            {
                Console.WriteLine("Currently in area: ROOT");
            }
            else
            {
                Console.WriteLine($"Current Area: ({currentGFileArea.Id}) { currentGFileArea.Title}");
            }
            Console.WriteLine("Child Areas: ");

            if (!gfileAreas.Any(p=>p.ParentAreaId==currentGFileArea?.Id))
            {
                Console.WriteLine("None");
            }
            else
            {
                if (currentGFileArea == null)
                {
                    foreach (var childArea in gfileAreas.Where(p=>p.ParentAreaId==null))
                    {
                        Console.WriteLine($"({childArea.Id}) { childArea.Title}");
                    }
                }
                else
                {
                    foreach (var childArea in currentGFileArea.ChildAreas)
                    {
                        Console.WriteLine($"({childArea.Id}) { childArea.Title}");
                    }
                }
            }
            Utils.Divider();
        }

        public void SetupGFileAreas()
        {
            var quitFlag = false;
            while (!quitFlag)
            {
                GFileHeader();
                if (currentGFileArea != null) Console.WriteLine("0. Navigate To Parent Area");
                Console.WriteLine("1. Add Child Area");
                if (currentGFileArea != null)
                {
                    Console.WriteLine("2. Edit This Area");
                    Console.WriteLine("3. Delete This Area");
                }
                if (gfileAreas.Any(p => p.ParentAreaId == currentGFileArea?.Id) || (currentGFileArea == null && gfileAreas.Any()))
                {
                    Console.WriteLine("4. Navigate To Child Area");
                }
                Console.WriteLine("I. Import Here.");
                Console.WriteLine("Q. Quit To Main Menu");
                var choice = Console.ReadLine();
                switch (choice.ToString().ToUpper())
                {
                    case "0":
                        //Change to parent area
                        currentGFileArea = gfileAreas.FirstOrDefault(p => p.Id == currentGFileArea.ParentAreaId);
                        break;
                    case "1":
                        CreateGFileArea();
                        break;
                    case "2":
                        //NOT IMPLEMENTED
                        break;
                    case "3":
                        //NOT IMPLEMENTED
                        break;
                    case "4":
                        int areaId = 0;
                        if (int.TryParse(Utils.Input("Enter the Id of the area to navigate to: ", ""), out areaId))
                        {
                            currentGFileArea = gfileAreas.FirstOrDefault(p => p.Id == areaId);
                        }
                        break;
                    case "I":
                        ImportGFiles();
                        break;
                    case "Q":
                        quitFlag = true;
                        break;
                    default:
                        break;
                }
            }

        }

        private  void CreateGFileArea()
        {
            GFileHeader();
            Console.WriteLine("ADD NEW AREA");
            var areaTitle = Utils.Input("Enter a name for this new area", "");
            if (areaTitle != "")
            {
                var areaDescription = Utils.Input("Enter a description for this new area", "");
                if (areaDescription != "")
                {
                   var parentId = currentGFileArea?.Id;
                    _core.CreateGFileArea(areaTitle, areaDescription, parentId);
                }
            }
        }

        private  void ImportGFiles()
        {
            GFileHeader();
            Console.WriteLine("IMPORT FILES");
            var filepath = Utils.Input("Enter the file path where the files and import.txt are", "");
            if (filepath != "")
            {
                var importFile = filepath + "import.txt";
                //Try to open import.txt
                if (File.Exists(importFile))
                {
                    var lines = File.ReadAllLines(importFile);
                    foreach (var line in lines){
                        var fields = line.Split('|');
                        _core.AddGFileDetail(currentGFileArea.Id, filepath, fields[0], fields[0], fields[2],"", false);
                        Console.WriteLine($"Imported {fields[0]}");
                    }
                }
                else
                {
                    Console.WriteLine("Import file not found at " + filepath);
                    Utils.EnterToContinue();
                }
            }
        }

    }
}
