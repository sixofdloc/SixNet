using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Net_Data;
using Net_Data.Classes;

namespace Net_BBS.BBS_Core
{
    class PFiles
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        private int Current_Pfile_Area = -1;
        private int Current_Parent_Area;

        private List<IdAndKeys> Current_Area_List = null;

        public PFiles(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            Current_Pfile_Area = -1;
            Current_Parent_Area = -1;
            _bbs.SendFileForTermType("pfile_entry_root", false);
            CMD_List();
        }

        public void Prompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
                _bbs.WriteLine("~l1~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                _bbs.Write("~c1PFiles~c2:~c7");
                string command = _bbs.Input(true, false, false);
                if (command.Length > 0)
                {
                    if ("0123456789".Contains(command.Substring(0, 1)))
                    {
                        //Select item.
                        IdAndKeys selectedItem = Current_Area_List.FirstOrDefault(p => p.Keys["listid"].Equals(command));
                        if (selectedItem != null)
                        {
                            if (selectedItem.Keys["type"] == "area")
                            {
                                ChangeToArea(selectedItem.Id);
                            }
                            else
                            {
                                //Run this file 
                                RunPFile(selectedItem.Keys["title"]);

                            }
                        }
                    }
                    else
                    {

                        switch (command.ToUpper()[0])
                        {
                            case 'H':
                                _bbs.SendFileForTermType("pfile_help", true);
                                break;
                            case 'L':
                                CMD_List();
                                break;
                            case '/':
                                if (Current_Pfile_Area > -1)
                                {
                                    ChangeToArea(_bbsDataCore.PFile_ParentArea(Current_Pfile_Area));
                                }
                                break;
                            case 'Q':
                                quitflag = true;
                                break;
                        }
                    }
                }
            }
        }


        public void ChangeToArea(int areaId)
        {
            //Select Area
            Current_Pfile_Area = areaId;
            if (areaId > 0)
            {
                _bbs.SendFileForTermType("pfile_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("pfile_entry_" + Current_Pfile_Area.ToString(), true);
            }
            CMD_List();

        }

        public void CMD_List()
        {
            _bbs.WriteLine("");
            _bbs.WriteLine("");
            Current_Area_List = _bbsDataCore.PFile_List_Area(Current_Pfile_Area);
            foreach (IdAndKeys idak in Current_Area_List)
            {
                _bbs.WriteLine(idak.Keys["listid"] + ". " + idak.Keys["desc"]);
            }
            _bbs.WriteLine("");
        }

        private void RunPFile(int pfilenum)
        {

        }

        private void RunPFile(string dllname)
        {
            Assembly assembly = Assembly.LoadFrom(dllname);
            MethodInfo method = null;
            Type pfiletype = null;
            int i = 0;
            while ((method == null) && i < assembly.GetTypes().Count())
            {
                Type t = assembly.GetTypes()[i];
                pfiletype = t;
                method = t.GetMethod("Main");
                i++;
            }
            Object[] param = new Object[] { _bbs, _bbsDataCore };
            var instance = Activator.CreateInstance(pfiletype, param);
            method.Invoke(instance, new object[] { });
        }

    }
}
