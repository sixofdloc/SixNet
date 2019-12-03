using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Net_Data;
using Net_Data.Models;

namespace Net_Setup.Classes.Setup
{
    public class PFilesSetup
    {
        private PFileArea currentPfileArea = null;
        private List<PFileArea> pfileAreas = null;

        private readonly BBSDataCore _core;

        public PFilesSetup(BBSDataCore core)
        {
            _core = core;
        }

        private void PFileHeader()
        {
            pfileAreas = _core.PFileAreas();
            Console.Clear();
            Console.WriteLine("Setup PFile Areas");
            Utils.Divider();
            if (currentPfileArea == null )//|| currentPfileArea.ParentAreaId == null)
            {
                Console.WriteLine("Currently in area: ROOT");
            }
            else
            {
                Console.WriteLine($"Current Area: ({currentPfileArea.Id}) { currentPfileArea.Title}");
            }
            Console.WriteLine("Child Areas: ");

            if (!pfileAreas.Any(p=>p.ParentAreaId==currentPfileArea?.Id))
            {
                Console.WriteLine("None");
            }
            else
            {
                if (currentPfileArea == null)
                {
                    foreach (var childArea in pfileAreas.Where(p=>p.ParentAreaId==null))
                    {
                        Console.WriteLine($"({childArea.Id}) { childArea.Title}");
                    }
                }
                else
                {
                    foreach (var childArea in currentPfileArea.ChildAreas)
                    {
                        Console.WriteLine($"({childArea.Id}) { childArea.Title}");
                    }
                }
            }
            Utils.Divider();
            Console.WriteLine("PFiles in this Area:");
            var pfileDetails = _core.PFileDetails(currentPfileArea?.Id);
            if (pfileDetails.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var pfile in pfileDetails)
                {
                    Console.WriteLine($"({pfile.Id}) {pfile.Title}");
                }
            }
            Utils.Divider();
        }

        public void SetupPFileAreas()
        {
            var quitFlag = false;
            while (!quitFlag)
            {
                PFileHeader();
                if (currentPfileArea != null) Console.WriteLine("0. Navigate To Parent Area");
                Console.WriteLine("1. Add Child Area");
                if (currentPfileArea != null)
                {
                    Console.WriteLine("2. Edit This Area");
                    Console.WriteLine("3. Delete This Area");
                }
                if (pfileAreas.Any(p => p.ParentAreaId == currentPfileArea?.Id) || (currentPfileArea == null && pfileAreas.Any()))
                {
                    Console.WriteLine("4. Navigate To Child Area");
                }
                Console.WriteLine("A. Add PFile Here.");
                Console.WriteLine("Q. Quit To Main Menu");
                var choice = Console.ReadLine();
                switch (choice.ToString().ToUpper())
                {
                    case "0":
                        //Change to parent area
                        currentPfileArea = pfileAreas.FirstOrDefault(p => p.Id == currentPfileArea.ParentAreaId);
                        break;
                    case "1":
                        CreatePFileArea();
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
                            currentPfileArea = pfileAreas.FirstOrDefault(p => p.Id == areaId);
                        }
                        break;
                    case "A":
                        AddPFile();
                        break;
                    case "Q":
                        quitFlag = true;
                        break;
                    default:
                        break;
                }
            }

        }

        private void CreatePFileArea()
        {
            PFileHeader();
            Console.WriteLine("ADD NEW AREA");
            var areaTitle = Utils.Input("Enter a name for this new area", "");
            if (areaTitle != "")
            {
                var areaDescription = Utils.Input("Enter a description for this new area", "");
                if (areaDescription != "")
                {
                   var parentId = currentPfileArea?.Id;
                    _core.CreatePFileArea(areaTitle, areaDescription, parentId);
                }
            }
        }
        private void AddPFile()
        {
            PFileHeader();
            Console.WriteLine("ADD PFILE");
            var filePath = Utils.Input("Enter the file path where the dll is located", "");
            if (filePath != "")
            {
                var dllFile = Utils.Input("Enter the name of the dll file", "");
                if (File.Exists(filePath + dllFile))
                {
                    var title = Utils.Input("Enter a short title for this pfile", "");
                    if (title != "")
                    {
                        var description = Utils.Input("Enter a long description for this pfile", "");
                        if (description != "")
                        {
                            _core.AddPFileDetail(currentPfileArea.Id, filePath, dllFile, title, description);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File not found.");
                    Utils.EnterToContinue();
                }
            }
        }

                //private static void ImportPFiles()
        //{
        //    PFileHeader();
        //    Console.WriteLine("IMPORT FILES");
        //    var filepath = Input("Enter the file path where the files and import.txt are", "");
        //    if (filepath != "")
        //    {
        //        var importFile = filepath + "import.txt";
        //        //Try to open import.txt
        //        if (File.Exists(importFile))
        //        {
        //            var lines = File.ReadAllLines(importFile);
        //            foreach (var line in lines){
        //                var fields = line.Split('|');
        //                _core.AddPFileDetail(currentPfileArea.Id, filepath, fields[0], fields[0], fields[2],"", false);
        //                Console.WriteLine($"Imported {fields[0]}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Import file not found at " + filepath);
        //            EnterToContinue();
        //        }
        //    }
        //}

    }
}
