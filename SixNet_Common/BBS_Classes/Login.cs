using System;
using SixNet_BBS.BBS_Classes;
using SixNet_Logger;
using SixNet_BBS_Data;

namespace SixNet_BBS
{

    class Login
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        public Login(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
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
                        uname = _bbs.Input(true, false, false,true,30);
                        if (uname.ToUpper() == "NEW")
                        {
                            break;
                        }
                        else
                        {
                            _bbs.Write("~l1~c1Password~c2: ~c7");
                            pass = _bbs.Input(true, true, false,false,30);
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
                            NewUser nu = new NewUser(_bbs,_dataInterface);
                            if(nu.Application()) 
                                u = _dataInterface.Login(nu.Username, nu.Password);
                            break;
                        }
                        else
                        {
                            //Username and pass have been filled in
                            u = _dataInterface.Login(uname, pass);
                            if (u !=null) break;
                        }

                    }
                    else
                    {
                        BuggerOff();
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
