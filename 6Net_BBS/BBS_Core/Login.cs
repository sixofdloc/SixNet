using System;
using Net_Data;
using Net_Data.Models;
using Net_Logger;

namespace Net_BBS.BBS_Core
{
    class Login
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        public Login(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
        }

        public User LogIn()
        {
            _bbs.CurrentArea = "Logging In";
            User u = null;
            try
            {
                string uname = "";
                string pass = "";
                int bigtries = 0;
                while (_bbs.Connected && bigtries < 3)
                {
                    int tries = 0;
                    while (_bbs.Connected && tries < 3)
                    {
                        _bbs.Write("~l2~c1Username~c2: ~c7");
                        uname = _bbs.Input(true, false, false, true, 30);
                        if (uname.ToUpper() == "NEW")
                        {
                            break;
                        }
                        else
                        {
                            _bbs.Write("~l1~c1Password~c2: ~c7");
                            pass = _bbs.Input(true, true, false, false, 30);
                            if ((uname == "") || (pass == ""))
                            {
                                tries++;
                            }
                            else break;
                        }
                    }
                    if (tries < 3)
                    {
                        if (uname.ToUpper() == "NEW")
                        {
                            NewUser nu = new NewUser(_bbs, _bbsDataCore);
                            if (nu.Application())
                                u = _bbsDataCore.Login(nu.Username, nu.Password);
                            break;
                        }
                        else
                        {
                            //Username and pass have been filled in
                            u = _bbsDataCore.Login(uname, pass);
                            if (u != null) break;
                        }

                    }
                    else
                    {
                        u = null; 
                        break;
                    }
                    if (u == null) bigtries++;
                }
                if (u != null)
                {
                    _bbs.WriteLine("~l1~c1Welcome, ~c7" + u.Username.ToUpper() + "~c1.~l4");
                }
                else
                {
                    BuggerOff();
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Login.LogIn: " + e.Message);
            }
            return u;
        }

        private void BuggerOff()
        {
            //Log off
            _bbs.WriteLine("~l1~c1Too many attempts.  ~l1Goodbye.~l1+++ATH0");
            _bbs.HangUp();
        }
    }
}
