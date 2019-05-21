using System;
using Net_Data;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    public class GraffitiWall
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        public GraffitiWall(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            _bbs.currentArea = "Graffiti Wall";
        }

        public void AddLine(int userid)
        {
            _bbs.currentArea = "Graffiti Wall, entering graffiti";
            _bbs.Write("~l1~c1~d0Enter your graffiti.  Please try to keep it to one line.  MCI is permitted.~l1~c7:~c1");
            String s = _bbs.Input(true, false, false);
            _bbsDataCore.AddGraffiti(s, userid);
            _bbs.WriteLine("~l1Your message has been added.");
        }

        public void DisplayWall()
        {
            var graffitiList = _bbsDataCore.GetGraffiti();
            if (_bbs.FileExistsForTermType("graffiti_top"))
            {
                _bbs.SendFileForTermType("graffiti_top", true);
            }
            else
            {
                _bbs.Write("~l2~d6" + Utils.Center("GRAFFITI WALL", _bbs.terminalType.Columns()) + "~d0");
            }
            if ((graffitiList != null) && (graffitiList.Count > 0))
            {
                foreach (var graffitiEntry in graffitiList)
                {
                    _bbs.WriteLine("~ce" + graffitiEntry.Item1 + "~c1:~c7" + graffitiEntry.Item2);
                }
            }
            else
            {
                _bbs.WriteLine("~c1Nothing Found");
            }
            _bbs.WriteLine("~d6" + Utils.SPC(_bbs.terminalType.Columns()) + "~d0");
        }

    }
}
