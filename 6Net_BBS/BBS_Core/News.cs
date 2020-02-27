using System;
using System.Collections.Generic;
using Net_Data;
using Net_Data.Models;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class News
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        public News(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
        }


        public void DisplayNews()
        {
            List<NewsItem> newsItems = _bbsDataCore.GetNews(_bbs.currentUser.LastDisconnection);
            if (_bbs.FileExistsForTermType("news_top"))
            {
                _bbs.SendFileForTermType("news_top", true);
            }
            else
            {
                _bbs.Write("~l2~d4" + Utils.Center("NEWS", _bbs.terminalType.Columns()) + "~d0");
            }
            if ((newsItems != null) && (newsItems.Count > 0))
            {
                foreach (NewsItem newsItem in newsItems)
                {
                    _bbs.WriteLine("~l1");
                    _bbs.WriteLine("~c1Subject: ~c7" + newsItem.Subject);
                    _bbs.WriteLine("~c1Date: ~c7" + newsItem.Posted.ToString("yy-MM-dd HH:mm") + "~c2");
                    _bbs.Write(Utils.Repeat('\xc0', _bbs.terminalType.Columns()));
                    _bbs.Write("~c1");
                    string[] splitarray = { "~\xff~" };
                    string[] lines = newsItem.Body.TrimEnd("~\xff".ToCharArray()).Split(splitarray, StringSplitOptions.None);
                    foreach (string s in lines)
                    {
                        if (s != "") _bbs.WriteLine(s);
                    }
                    _bbs.Write("~c4");
                }
            }
            else
            {
                _bbs.WriteLine("~c1Nothing Found");
            }
            _bbs.WriteLine("~d4" + Utils.Center("END OF NEWS", _bbs.terminalType.Columns()) + "~d0");
        }


        public void Prompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
                _bbs.WriteLine("~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7R~c1ead~c2, ~c7Q~c1uit");
                _bbs.Write("~c1News~c2: ~c7:");
                string command = _bbs.Input(true, false, false);
                switch (command.ToUpper()[0])
                {
                    case 'B':
                        break;
                    case 'U':
                        break;
                    case 'G':
                        break;
                    case 'P':
                        break;
                    case 'Q':
                        quitflag = true;
                        break;
                }
            }

        }

    }
}
