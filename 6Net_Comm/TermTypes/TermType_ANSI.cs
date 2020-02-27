using System;
using Net_Comm.Consts;
using Net_Comm.Interfaces;

namespace Net_Comm.TermTypes
{
    public class TermType_ANSI : ITerminalType
    {
        public string TerminalTypeName()
        {
            return "ANSI";
        }

        public string Horizontal_Bar()
        {
            return "\xc4";
        }

        public string CRLF()
        {
            return "\r\n";
        }

        public char BACKSPACE()
        {
            return '\b';
        }

        public char Input_Terminator()
        {
            return '\r';
        }

        public string Filename_Prefix()
        {
            return "Systext\\";
        }


        public string MCI(string s)
        {
            string t = s;
            t = t.Replace("~s1", "\x0c");
            t = t.Replace("~g1", "\x07");
            t = t.Replace("~c0", ANSI.COLOR_BLACK); //black
            t = t.Replace("~c1", ANSI.COLOR_BRT_WHITE); //white
            t = t.Replace("~c2", ANSI.COLOR_BRT_RED); //red
            t = t.Replace("~c3", ANSI.COLOR_BRT_CYAN); //cyan
            t = t.Replace("~c4", ANSI.COLOR_MAGENTA); //purple
            t = t.Replace("~c5", ANSI.COLOR_GREEN); //green
            t = t.Replace("~c6", ANSI.COLOR_BLUE); //blue
            t = t.Replace("~c7", ANSI.COLOR_BRT_YELLOW); //yellow
            t = t.Replace("~c8", ANSI.COLOR_RED); //orange
            t = t.Replace("~c9", ANSI.COLOR_YELLOW); //brown
            t = t.Replace("~ca", ANSI.COLOR_BRT_MAGENTA); //pink
            t = t.Replace("~cb", ANSI.COLOR_BRT_BLACK); //grey1
            t = t.Replace("~cc", ANSI.COLOR_WHITE); //grey2
            t = t.Replace("~cd", ANSI.COLOR_BRT_GREEN); //ltgreen
            t = t.Replace("~ce", ANSI.COLOR_CYAN); //ltblue
            t = t.Replace("~cf", ANSI.COLOR_BRT_WHITE); //grey3
            t = t.Replace("~d0", ANSI.BACKGROUND_BLACK); //black
            t = t.Replace("~d1", ANSI.BACKGROUND_BRT_WHITE); //white
            t = t.Replace("~d2", ANSI.BACKGROUND_BRT_RED); //red
            t = t.Replace("~d3", ANSI.BACKGROUND_BRT_CYAN); //cyan
            t = t.Replace("~d4", ANSI.BACKGROUND_MAGENTA); //purple
            t = t.Replace("~d5", ANSI.BACKGROUND_GREEN); //green
            t = t.Replace("~d6", ANSI.BACKGROUND_BLUE); //blue
            t = t.Replace("~d7", ANSI.BACKGROUND_BRT_YELLOW); //yellow
            t = t.Replace("~d8", ANSI.BACKGROUND_RED); //orange
            t = t.Replace("~d9", ANSI.BACKGROUND_YELLOW); //brown
            t = t.Replace("~da", ANSI.BACKGROUND_BRT_MAGENTA); //pink
            t = t.Replace("~db", ANSI.BACKGROUND_BRT_BLACK); //grey1
            t = t.Replace("~dc", ANSI.BACKGROUND_WHITE); //grey2
            t = t.Replace("~dd", ANSI.BACKGROUND_BRT_GREEN); //ltgreen
            t = t.Replace("~de", ANSI.BACKGROUND_CYAN); //ltblue
            t = t.Replace("~df", ANSI.BACKGROUND_BRT_WHITE); //grey3

            while (t.Contains("~l"))
            {
                string l = t.Substring(t.IndexOf("~l"), 3);
                string p = t.Substring(0, t.IndexOf("~l"));
                string q = "";
                if (t.IndexOf("~l") + 3 < t.Length)
                {
                    q = t.Substring(t.IndexOf("~l") + 3, t.Length - 3);
                }
                if (l[2] >= '0' && l[2] <= '9')
                {
                    string r = "";
                    for (int i = 0; i < (int.Parse(l[2].ToString())); i++)
                    {
                        r = r + CRLF();
                    }
                    t = p + r + q;
                }
                else
                {
                    t = p + q;
                }
            }
            return t;
        }

        public string TranlateToTerminal(string s)
        {
            return s;
        }

        public char TranslateToTerminal(char c)
        {
            return c;
        }

        public char TranslateFromTerminal(char c)
        {
            return c;
        }


        public string Filename_Suffix()
        {
            return ".ans";
        }

        public int Columns()
        {
            return 80;
        }

        public bool Linefeeds()
        {
            return true;
        }

        public bool ANSI_Color()
        {
            return true;
        }

        public bool C64_Color()
        {
            return false;
        }

    }
}
