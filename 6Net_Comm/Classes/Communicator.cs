using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Net_Comm.Interfaces;
using Net_Logger;

namespace Net_Comm.Classes
{
    public class Communicator
    {
        public StateObject _stateObject { get; set; }
        public ITerminalType terminalType { get; set; }
        public DateTime lastActivity { get; set; }
        public byte currentChar { get; set; } //For prompts

        public bool Connected
        {
            get => _stateObject.connected;
        }  //Flag for loops 


        public void HangUp()
        {
            _stateObject.workSocket.Disconnect(false);
        }


        #region Communications Routines

        public bool FileExistsForTermType(string filename)
        {
            string fname = terminalType.Filename_Prefix() + filename + terminalType.Filename_Suffix();
            return File.Exists(fname);
        }
        public void SendFileForTermType(string filename, bool raw)
        {
            string fname = terminalType.Filename_Prefix() + filename + terminalType.Filename_Suffix();
            SendFile(fname, raw);
        }

        public void SendFile(string filename, bool raw)
        {
            raw = true;
            if (File.Exists(filename))
            {
                if (raw)
                {
                    byte[] bytes = File.ReadAllBytes(filename);
                    _stateObject.workSocket.Send(bytes);
                    // WriteRaw(text);
                }
                else
                {
                    string[] lines = File.ReadAllText(filename).Split('\n');
                    foreach (string s in lines) WriteLine(s);
                }
            }
            else
            {
                LoggingAPI.Error("File Not Found: " + filename);
            }
        }

        public void WriteRaw(string s)
        {
            foreach (char c in s) ByteOut((byte)c);
        }

        public void WriteByte(byte b)
        {
            _stateObject.workSocket.Send(new byte[] { b });
        }

        public void WriteLine()
        {
            Write(terminalType.CRLF());
        }

        public void WriteLine(string s)
        {
            Write(s);
            Write(terminalType.CRLF());
        }

        public void WriteString(string s)
        {
            byte[] ca = s.Select(p => (byte)p).ToArray();
            if (_stateObject.connected)
            {
                _stateObject.workSocket.Send(ca);
            }
        }

        public void Write(string stringToWrite)
        {
            try
            {
                string translatedStringToWrite = terminalType.TranslateToTerminal(stringToWrite);
                for (int i = 0; i < translatedStringToWrite.Length && _stateObject.connected; i++)
                {
                    if (translatedStringToWrite.Substring(i, 1) != "~")
                    {
                        WriteString(translatedStringToWrite.Substring(i, 1));
                    }
                    else
                    {
                        string v = translatedStringToWrite.Substring(i, 2);
                        string u = translatedStringToWrite.Substring(i + 2, 1);
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
                                WriteString(terminalType.MCI(v + u));
                                break;
                        }
                        i = i + 2;
                    }
                }
            }
            catch (SocketException socketException){
                LoggingAPI.Exception(socketException, new { stringToWrite }); 
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { stringToWrite });
            }
        }

        public void ByteOut(byte b)
        {
            _stateObject.workSocket.Send(new byte[] { b });
        }

        public void BytesOut(byte[] b)
        {
            _stateObject.workSocket.Send(b);
        }

        public void Chrout(char c)
        {
            if (c != '\x0')
            {
                char d = terminalType.TranslateToTerminal(c);
                _stateObject.workSocket.Send(new byte[] { (byte)d });
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
            Chrout(terminalType.BACKSPACE());
            Chrout(' ');
            Chrout(terminalType.BACKSPACE());
        }

        public void Chrsout(char c, int x, bool translate)
        {
            if (c != '\x0')
            {
                for (int i = 0; i < x; i++)
                {
                    char d = translate ? terminalType.TranslateToTerminal(c) : c;
                    _stateObject.workSocket.Send(new byte[] { (byte)d });
                }
            }
        }

        public void Divider()
        {
            Chrsout('\xc0', terminalType.Columns(), false);
        }

        public void Exclaim(string s)
        {
            WriteLine("~l1~d2" + s + "~d0~g1");
        }

        public char GetChar()
        {
            currentChar = 0x00;
            while ((currentChar == 0x00) && (Connected) && _stateObject.connected)
            {
                Thread.Sleep(10);
            }
            return terminalType.TranslateFromTerminal((char)currentChar); //Returns \x0 on disconnect
        }

        public void AnyKey(bool Prompt, bool newline)
        {
            if (newline) Write("~l1");
            if (Prompt) Write("~d1hit any key to continue~d0");
            GetChar();
            if (this.terminalType.C64_Color()) WriteByte(0x0e);
        }

        public bool MoreOrAbort()
        {
            Write("~c7M~c1ore,~c7A~c1bort:~c1");
            char c = GetChar();
            if (c.ToString().ToUpper() == "A" || c == '\x1b')
            {
                WriteLine("Abort");
                return false;
            }
            if (c.ToString().ToUpper() == "M" || c == ' ' || c == '\n')
            {
                WriteLine("More");
                return true;
            }
            return true;
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
                if (c == terminalType.BACKSPACE())
                {
                    if (s.Length == 0)
                    {
                        Write("~g1");
                    }
                    else
                    {
                        Chrout(terminalType.BACKSPACE());
                        s = s.Substring(0, s.Length - 1);
                    }
                }
                else
                {
                    if (c != terminalType.Input_Terminator())
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
            currentChar = key;
            lastActivity = DateTime.Now;
        }

        public void Disconnect()
        {
            _stateObject.Disconnect();
        }
        #endregion

    }
}
