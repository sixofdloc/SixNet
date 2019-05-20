using System;
using System.Collections.Generic;
using Net_Data;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class LastTen
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        public LastTen(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
        }

        public void ShowLastTenCalls()
        {
            _bbs.CurrentArea = "Viewing Last 10 Callers";
            var callList = _bbsDataCore.GetLastTenCalls();
            if ((callList != null) && (callList.Count > 0))
            {
                if (_bbs.FileExistsForTermType("last10_top"))
                {
                    _bbs.SendFileForTermType("last10_top", true);
                }
                else
                {
                    _bbs.Write("~s1~d5" + Utils.Center("LAST 10 CALLERS", _bbs.terminalType.Columns()) + "~d0");
                }
                foreach (var call in callList)
                {
                    _bbs.WriteLine("~cd" + call.Item1 + "~c1:~c7" + call.Item2);
                }

                _bbs.WriteLine("~d5" + Utils.SPC(_bbs.terminalType.Columns()) + "~d0");
            }
        }



    }
}
