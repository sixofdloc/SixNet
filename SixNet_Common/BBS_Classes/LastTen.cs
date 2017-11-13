using System.Collections.Generic;
using SixNet_BBS_Data;
using SixNet_StringUtils;

namespace SixNet_BBS.BBS_Classes
{
    class LastTen
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        public LastTen(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
        }

        public void ShowLast10()
        {
            _bbs.CurrentArea = "Viewing Last 10 Callers";
            List<Dictionary<string, string>> glist = _dataInterface.GetLast10();
            if ((glist != null) && (glist.Count > 0))
            {
                if (_bbs.FileExistsForTermType("last10_top"))
                {
                    _bbs.SendFileForTermType("last10_top", true);
                }
                else
                {
                    _bbs.Write("~s1~d5" + Utils.Center("LAST 10 CALLERS", _bbs.TerminalType.Columns()) + "~d0");
                }
                foreach (Dictionary<string, string> dic in glist)
                {
                    _bbs.WriteLine("~cd" + dic["when"] + "~c1:~c7" + dic["user"]);
                }
                
               _bbs.WriteLine("~d5" + Utils.SPC(_bbs.TerminalType.Columns()) + "~d0");
            }
        }



    }
}
