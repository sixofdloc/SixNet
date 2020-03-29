using System;
using System.Collections.Generic;
using Net_Logger;

namespace Net_BBS.BBS_Core.Editors
{
    class Line_Editor
    {
        private readonly BBS _bbs;

        public List<String> Message { get; set; }
        private int CurrentLine ;
        private int Column ;
        private bool ExitFlag ;

        public string GetMessage()
        {
            //The system internally uses ~\xff~ to denote a line ending
            string s = "";
            foreach (String t in Message)
            {
                s = s + t + "~\xff~";
            }
            return s;
        }

        public Line_Editor(BBS bbs)
        {
            _bbs = bbs;
        }

        public bool Edit(List<String> message)
        {
            _bbs.doNotDisturb = true;
            bool result = false;
            try
            {
                Header();
                ExitFlag = false;
                CurrentLine = 0;
                if ((message == null) || (message.Count == 0))
                {
                    //No message to edit - new\
                    Message = new List<String>
                    {
                        ""
                    };
                }
                else
                {
                    Message = message;
                    CMD_Preview();
                    Message.Add("");
                    CurrentLine = Message.Count - 1;
                }

                while (!ExitFlag)
                {
                    //Getchar
                    char ch = _bbs.GetChar();
                    //Is this the first char in a blank line?
                    if (ch == '.')
                    {
                        if (Column == 0)
                        {
                            _bbs.Write("Command:");
                            ch = _bbs.GetChar();
                            switch (ch.ToString().ToUpper())
                            {
                                case "A":
                                    CMD_Abort();
                                    if (ExitFlag) result = false;
                                    break;
                                case "H":
                                    CMD_Help();
                                    break;
                                case "P":
                                    CMD_Preview();
                                    break;
                                case "R":
                                    CMD_Read();
                                    break;
                                case "S":
                                    CMD_Save();
                                    if (ExitFlag) result = true;
                                    break;
                                case ".":
                                    _bbs.Backspaces(8);
                                    _bbs.Chrout('.');
                                    break;
                                default:
                                    _bbs.Backspaces(8);
                                    break;
                            }
                        }
                        else
                        {
                            //Just drawing a .
                            NormalChar('.');
                        }
                    }
                    else
                    {
                        ////Nomal character,
                        //if (ch == '\x1b')
                        //{
                        //    ch = Host_System.GetChar();
                        //    if (ch == '[')
                        //    {
                        //        ch = Host_System.GetChar();
                        //        switch (ch)
                        //        {
                        //            case 'A':
                        //                Cursor_Up();
                        //                break;
                        //            case 'B':
                        //                Cursor_Down();
                        //                break;
                        //            case 'C':
                        //                Cursor_Right();
                        //                break;
                        //            case 'D':
                        //                Cursor_Left();
                        //                break;
                        //            case 'H':
                        //                Cursor_Home();
                        //                break;
                        //            default:
                        //                break;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        if (ch == _bbs.terminalType.BACKSPACE())
                        {
                            Backspace();
                        }
                        else
                        {
                            if (ch == 0x0d)
                            {
                                CarriageReturn(true);
                            }
                            else
                            {
                                //Normal chars
                                NormalChar(ch);
                            }
                        }

                        //}
                    } //Compare to '.'

                } //While Exitflag
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { message });
            }
            _bbs.doNotDisturb = _bbs.overrideDoNotDisturb;
            return result;
        }

        private void Header()
        {
            if (_bbs.terminalType.Columns() == 80)
            {
                _bbs.WriteLine("~s1 ~c1Line Editor ~c7|~c2.H ~c1on blank line for help~c7, ~c2.S ~c1to save~c7, ~c2.A ~c1to abort");
                _bbs.Write("~c7          1111111111222222222233333333334444444444555555555566666666667777777777");
                _bbs.Write("01234567890123456789012345678901234567890123456789012345678901234567890123456789~c1");
            }
            else
            {
                _bbs.WriteLine("~s1 ~c1 Line Editor ~c7|~c2.H ~c1for help~c7");
                _bbs.Write("~c7          111111111122222222223333333333");
                _bbs.Write("0123456789012345678901234567890123456789~c1");
            }

        }

        private void Backspace()
        {
            if (Column > 0)
            {
                _bbs.Backspace();

                Message[CurrentLine] = Message[CurrentLine].Substring(0, Message[CurrentLine].Length - 1);
                Column--;
            }
        }

        private void CarriageReturn(bool visual)
        {
            Column = 0;
            CurrentLine++;
            if (Message.Count < CurrentLine + 1) Message.Add("");
            if (visual) _bbs.WriteLine("");
        }

        private void NormalChar(char ch)
        {
            //            bbs.Chrout(bbs.TerminalType.TranslateFromTerminal(ch));
            _bbs.Chrout(ch);

            Message[CurrentLine] += ch;// Host_System.TerminalType.TranslateFromTerminal(ch);
            Column++;
            if (Column == _bbs.terminalType.Columns())
            {
                CarriageReturn(false);
            }
        }

        #region Editor Commands
        private void CMD_Abort()
        {
            char ch;
            _bbs.Write("Abort");
            ch = _bbs.GetChar();
            if (ch == '\r')
            {
                _bbs.Write("~l1Are you sure?");
                if (_bbs.YesNo(true, true))
                {

                    ExitFlag = true;
                }
                else
                {
                    CMD_Preview();
                }
            }
            else
            {
                _bbs.Backspaces(13);
            }
        }

        private void CMD_Preview()
        {
            char ch;
            _bbs.Write("Preview");
            ch = _bbs.GetChar();
            if (ch == '\r')
            {
                Header();
                for (int i = 0; i < Message.Count; i++)
                {
                    _bbs.WriteLine(Message[i]);
                }
                _bbs.WriteLine("");
            }
            else
            {
                _bbs.Backspaces(15);
            }

        }
        private void CMD_Read()
        {
            char ch;
            _bbs.Write("Read");
            ch = _bbs.GetChar();
            if (ch == '\r')
            {
                Header();
                for (int i = 0; i < Message.Count; i++)
                {
                    _bbs.WriteRaw(Message[i]);
                    _bbs.WriteLine("");
                }
                _bbs.WriteLine("");
            }
            else
            {
                _bbs.Backspaces(15);
            }

        }

        private void CMD_Help()
        {
            char ch;
            _bbs.Write("Help");
            ch = _bbs.GetChar();
            if (ch == '\r')
            {
                _bbs.SendFileForTermType("line_editor_help", false);
                _bbs.AnyKey(true, true);
                CMD_Preview();
            }
            else
            {
                _bbs.Backspaces(12);
            }
        }

        private void CMD_Save()
        {
            char ch;
            _bbs.Write("Save");
            ch = _bbs.GetChar();
            if (ch == '\r')
            {
                ExitFlag = true;
            }
            else
            {
                _bbs.Backspaces(12);
            }
        }
        #endregion


        private void Cursor_Up()
        {
        }
        private void Cursor_Down()
        {
        }
        private void Cursor_Left()
        {
        }
        private void Cursor_Right()
        {
        }
        private void Cursor_Home()
        {
        }

    }
}
