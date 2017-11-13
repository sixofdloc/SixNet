using System;
using SixNet_BBS.BBS_Classes.Editors;
using SixNet_Logger;
using SixNet_StringUtils;
using SixNet_BBS_Data;

namespace SixNet_BBS.BBS_Classes
{
    class NewUser
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        public string Username      { get; set; }
        public string Password      { get; set; }
        public string RealName      { get; set; }
        public string Email         { get; set; }
        public string ComputerType  { get; set; }


        private int tries = 0;
        private bool validinput = false;



        public NewUser(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
        }

        public bool Application()
        {
            bool result = false;
            bool quitflag = false;
            try
            {
                //Show new user file
                _bbs.SendFileForTermType("NewUser", true);
                if (!GetUsername()) return false;
                if (!GetPassword()) return false;
                RealName = GetField("Real Name", "~l2~c11Enter your real name.", 0);
                if (RealName == null) return false;
                Email = GetField("Email", "~l2~c11Enter your email address.", 0);
                if (Email == null) return false;
                ComputerType = GetField("Computer", "~l2~c11Enter your computer model.", 0);
                if (ComputerType == null) return false;
                _dataInterface.SaveNewUser(Username, Password, RealName, Email, ComputerType);

                    //New User feedback
                _bbs.Write("~l2~c11Leave an introduction message?");
                if (_bbs.YesNo(true, true))
                {
                    Line_Editor e = new Line_Editor(_bbs);
                    if (e.Edit(null))
                    {
                        _dataInterface.NewFeedback("New User Feedback", e.GetMessage(), -1);
                    }
                }
                    //And Done
                    result = true;

            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in NewUser.Application: " + e.ToString());
            }
            return result;
            
        }

        private bool GetUsername()
        {
            bool result = false;
            //Prompt for username
            validinput = false;
            tries = 0;

            while (!validinput && (tries < 5))
            {
                _bbs.WriteLine("~l2~c1Enter a username to use on this system");
                _bbs.Write("~c1Username~c2:~c7");
                Username = _bbs.Input(true, false, false);
                //Validate username. No dupes (case-insensitive)
                if (Utils.CheckForSQL(Username))
                {
                    _bbs.WriteLine("~s1~c2You're a real fucking asshole.  Beat it.");
                    validinput = false;
                    tries = 99;
                }
                else
                {
                    if (_dataInterface.ValidNewUsername(Username))
                    {
                        validinput = true;
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~c2Invalid or duplicate username.");
                        tries++;
                    }
                }
            }
            result = validinput;
            return result;
        }

        private bool GetPassword()
        {
            bool result = false;
            //Prompt for username
            validinput = false;
            tries = 0;
            while (!validinput && (tries < 5))
            {
                _bbs.WriteLine("~l2~c1Enter a password to use on this system.");
                _bbs.Write("~c1Password~c2:~c7");
                Password = _bbs.Input(true, true, false);
                if (Utils.CheckForSQL(Password))
                {
                    _bbs.WriteLine("~s1~c2You're a real fucking asshole.  Beat it.~c1");
                    validinput = false;
                    tries = 99;
                }
                //Validate password, at least 8 characters
                if (Password.Length>=8)
                {
                    //And validate
                    _bbs.WriteLine("~l1~c1Re-enter that password to verify.");
                    _bbs.Write("~c1Verify~c2:~c7");
                    string matchpassword = _bbs.Input(true, true, false);
                    if (matchpassword == Password)
                    {
                        validinput = true;
                    }
                    else
                    {
                        _bbs.WriteLine("~s1~c2Passwords don't match.  Try again.~c1");
                        tries++;
                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~c2Password must be at least 8 characters.");
                    tries++;
                }
            }
            result = validinput;
            return result;
        }


        private string GetField(string fieldname, string prompt, int minimumlen)
        {
            string result = "";
            validinput = false;
            tries = 0;

            while (!validinput && (tries < 5))
            {
                _bbs.WriteLine("~l1~c1"+prompt);
                _bbs.Write("~c1"+fieldname+"~c2:~c7");
                result = _bbs.Input(true, false, false);
                if (Utils.CheckForSQL(result))
                {
                    _bbs.WriteLine("~s1~c2You're a real fucking asshole.  Beat it.");
                    validinput = false;
                    tries = 99;
                }
                //Validate password, at least x characters
                if (minimumlen > 0)
                {
                    if (result.Length >= minimumlen)
                    {
                        validinput = true;
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~c2" + fieldname + " must be at least " + minimumlen.ToString() + " characters.");
                        tries++;
                    }
                }
                else
                {
                    validinput = true;
                }
            }
            if (!validinput) result = null;
            return result;
        }

    }
}
