using System;
using Net_Comm.Interfaces;
using Net_Comm.TermTypes;
using Net_Logger;

namespace Net_BBS.BBS_Core
{
    class TermDetect
    {
        private BBS Host_System { get; set; }

        public TermDetect(BBS bbs)
        {
            Host_System = bbs;
        }

        public I_TerminalType Detect()
        {
            I_TerminalType termtype = new TermType_Default();
            try
            {
                bool detected = false;
                while ((!detected) && Host_System.Connected)
                {
                    Host_System.Write("\x93\x0cHit Delete:");
                    char c = Host_System.GetChar();
                    if ((c == 8))
                    {
                        Host_System.WriteLine("ASCII/ANSI Detected");
                        Host_System.Write("ANSI Color? ");
                        bool b = Host_System.YesNo(true, true);
                        if (b)
                        {
                            termtype = new TermType_ANSI();
                            detected = true;
                        }
                        else
                        {
                            termtype = new TermType_Default();
                            detected = true;
                        }
                    }
                    else
                    {
                        if (c == '\x14')
                        {
                            termtype = new TermType_PETSCII40();
                            Host_System.WriteRaw("\x1cp" + "\x81" + "e\x9et" + "\x99s" + "\x9a" + "c\x9c" + "i\x1ci \x05" + "dETECTED\x02!\x05");
                            detected = true;
                        }
                        else
                        {
                            if (c == 127)
                            {
                                Host_System.WriteRaw("\x1b[1;31mA\x1b[1;32mN\x1b[1;33mS\x1b[1;34mI\x1b[1;37m Detected");
                                termtype = new TermType_ANSI();
                                detected = true;
                            }
                            else
                            {
                                Host_System.WriteLine(((int)c).ToString());
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in TermDetect.Detect: " + e.Message);
            }
            return termtype;
        }

    }

}
