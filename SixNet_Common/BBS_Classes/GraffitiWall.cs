using System;
using System.Collections.Generic;
using SixNet_BBS_Data;
using SixNet_StringUtils;

namespace SixNet_BBS.BBS_Classes
{
    public class GraffitiWall
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        public GraffitiWall(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            _bbs.CurrentArea = "Graffiti Wall";
        }

        public void AddLine(int userid)
        {
            _bbs.CurrentArea = "Graffiti Wall, entering graffiti";
            _bbs.Write("~l1~c1~d0Enter your graffiti.  Please try to keep it to one line.  MCI is permitted.~l1~c7:~c1");
            String s = _bbs.Input(true, false, false);
            _dataInterface.AddGraffiti(s, userid);
            _bbs.WriteLine("~l1Your message has been added.");
        }

        public void DisplayWall()
        {
            List<Dictionary<string,string>> glist = _dataInterface.GetGraffiti();
            if (_bbs.FileExistsForTermType("graffiti_top"))
            {
                _bbs.SendFileForTermType("graffiti_top",true);
            }
            else
            {
                _bbs.Write("~l2~d6" + Utils.Center("GRAFFITI WALL", _bbs.TerminalType.Columns()) + "~d0");
            }
            if ((glist != null) && (glist.Count > 0))
            {
                foreach (Dictionary<string, string> dic in glist)
                {
                    _bbs.WriteLine("~ce" + dic["user"] + "~c1:~c7" + dic["graf"]);
                }
            }
            else
            {
                _bbs.WriteLine("~c1Nothing Found");
            }
            _bbs.WriteLine("~d6" + Utils.SPC(_bbs.TerminalType.Columns()) + "~d0");
        }

    }
}
