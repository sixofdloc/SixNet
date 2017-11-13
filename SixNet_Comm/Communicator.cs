using System;
using System.Linq;
using System.IO;
using SixNet_Logger;
using System.Threading;

namespace SixNet_Comm
{
    public class Communicator
    {
        public StateObject State_Object { get; set; }

        public byte Currentchar { get; set; } //For prompts
        public bool Connected { get; set; }  //Flag for loops 
        public I_TerminalType TerminalType { get; set; }
        public DateTime LastActivity { get; set; }


        public void HangUp()
        {
            State_Object.workSocket.Disconnect(false);
        }


        #region Communications Routines

        public bool FileExistsForTermType(string filename)
        {
            string fname = TerminalType.Filename_Prefix() + filename + TerminalType.Filename_Suffix();
            return File.Exists(fname);
        }
        public void SendFileForTermType(string filename, bool raw)
        {
            string fname = TerminalType.Filename_Prefix() + filename + TerminalType.Filename_Suffix();
            SendFile(fname, raw);
        }

        public void SendFile(string filename, bool raw)
        {
            if (File.Exists(filename))
            {
                if (raw)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(filename);
                    State_Object.workSocket.Send(bytes);
                    // WriteRaw(text);
                }
                else
                {
                    string[] lines = System.IO.File.ReadAllLines(filename);
                    foreach (string s in lines) WriteLine(s);
                }
            }
            else
            {
                LoggingAPI.LogEntry("~l1File Not Found: " + filename);
            }
        }

        public void WriteRaw(string s)
        {
            foreach (char c in s) ByteOut((byte)c);
        }


        public void WriteLine()
        {
            Write(TerminalType.CRLF());
        }

        public void WriteLine(string s)
        {
            Write(s);
            Write(TerminalType.CRLF());
        }

        public void WriteString(string s)
        {
            byte[] ca = s.Select(p => (byte)p).ToArray();
            State_Object.workSocket.Send(ca);
        }

        public void Write(string s)
        {
            try
            {
                string t = TerminalType.TranlateToTerminal(s);
                for (int i = 0; i < t.Length; i++)
                {
                    if (t.Substring(i, 1) != "~")
                    {
                        WriteString(t.Substring(i, 1));
                    }
                    else
                    {
                        string v = t.Substring(i, 2);
                        string u = t.Substring(i + 2, 1);
                        switch (v.ToUpper())
                        {
                            case "~K":
                                if (u == "0")
                                {
                                    AnyKey(false, false);
                                }
                                else
                                {
                                    AnyKey(true, false);
                                }
                                break;
                            case "~P":
                                int x = int.Parse(u) * 1000;
                                Thread.Sleep(x);
                                break;
                            default:
                                WriteString(TerminalType.MCI(v + u));
                                break;
                        }
                        i = i + 2;
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Communicator.Write: " + e);
            }
        }

        public void ByteOut(byte b)
        {
            State_Object.workSocket.Send(new byte[] { b });
        }

        public void BytesOut(byte[] b)
        {
            State_Object.workSocket.Send(b);
        }

        public void Chrout(char c)
        {
            if (c != '\x0')
            {
                char d = TerminalType.TranslateToTerminal(c);
                State_Object.workSocket.Send(new byte[] { (byte)d });
            }
        }

        public void Backspaces(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Backspace();
            }
        }

        public void Backspace()
        {
            Chrout(TerminalType.BACKSPACE());
            Chrout(' ');
            Chrout(TerminalType.BACKSPACE());
        }

        public void Chrsout(char c, int x, bool translate)
        {
            if (c != '\x0')
            {
                for (int i = 0; i < x; i++)
                {
                    char d = translate ? TerminalType.TranslateToTerminal(c) : c;
                    State_Object.workSocket.Send(new byte[] { (byte)d });
                }
            }
        }

        public void Divider()
        {
            Chrsout('\xc0', TerminalType.Columns(), false);
        }

        public void Exclaim(string s)
        {
            WriteLine("~l1~d2" + s + "~d0~g1");
        }

        public char GetChar()
        {
            Currentchar = 0x00;
            while ((Currentchar == 0x00) && (Connected))
            {
                Thread.Sleep(10);
            }
            return TerminalType.TranslateFromTerminal((char)Currentchar); //Returns \x0 on disconnect
        }

        public void AnyKey(bool Prompt, bool newline)
        {
            if (newline) Write("~l1");
            if (Prompt) Write("~d1Hit any key to continue~d0");
            GetChar();
        }

        public bool YesNo(bool Prompt, bool ShowResponse)
        {
            bool b = false;
            bool gotresponse = false;
            if (Prompt) Write("(Y/N)");
            while ((!gotresponse) && Connected)
            {
                char c = GetChar();
                if ("yYnN".Contains(c))
                {
                    b = ("yY".Contains(c));
                    gotresponse = true;
                }
            }
            if (ShowResponse)
            {
                if (b)
                {
                    WriteLine("Yes");
                }
                else
                {
                    WriteLine("No");
                }
            }
            return b;
        }

        public string Input()
        {
            return Input(true, false, false, false, 0);
        }

        public int InputNumber(int digits)
        {
            string r = Input(true, false, true, false, digits);
            if (int.TryParse(r, out int i))
            {
                return i;
            }
            else
            {
                return 0;
            }
        }

        public string Input(bool echo)
        {
            return Input(echo, false, false, false, 0);
        }

        public string Input(bool echo, bool password)
        {
            return Input(echo, password, false, false, 0);
        }

        public string Input(bool echo, bool password, bool numbersonly)
        {

            return Input(echo, password, numbersonly, false, 0);
        }

        public string Input(bool echo, bool password, bool numbersonly, bool allcaps)
        {
            return Input(echo, password, numbersonly, allcaps, 0);
        }

        private char[] numchars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '-' };

        public string Input(bool echo, bool password, bool numbersonly, bool allcaps, int maxchars)
        {
            string s = "";
            bool done = false;
            if (maxchars == 0) maxchars = 65535;
            while ((!done))
            {
                char c = GetChar();
                if (allcaps) c = c.ToString().ToUpper()[0];
                if (c == TerminalType.BACKSPACE())
                {
                    if (s.Length == 0)
                    {
                        Write("~g1");
                    }
                    else
                    {
                        Chrout(TerminalType.BACKSPACE());
                        s = s.Substring(0, s.Length - 1);
                    }
                }
                else
                {
                    if (c != TerminalType.Input_Terminator())
                    {
                        if (s.Length < maxchars)
                        {
                            if (numbersonly)
                            {
                                if (numchars.Contains(c))
                                {
                                    s = s + c;
                                }
                                else
                                {
                                    Write("~g1");
                                }
                            }
                            else
                            {
                                s = s + c;
                            }
                            if (echo)
                            {
                                if (password)
                                {
                                    Chrout('*');
                                }
                                else
                                {
                                    Chrout(c);
                                }
                            }
                        }
                        else
                        {
                            Write("~g1");
                        }
                    }
                    else done = true;
                }
            }
            return s;
        }

        #endregion

        #region Callbacks from StateObject/Socket
        public void Receive(byte key)
        {
            Currentchar = key;
            LastActivity = DateTime.Now;
        }

        public void Disconnect()
        {
            Connected = false;
        }
        #endregion

    }
}
