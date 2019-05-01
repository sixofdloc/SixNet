using System;
using Net_Comm.Interfaces;

namespace Net_Comm.TermTypes
{
    public class TermType_Default : I_TerminalType
    {
        public string TerminalTypeName()
        {
            return "Default";
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
            return '\n';
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
            t = t.Replace("~c0", ""); //black
            t = t.Replace("~c1", ""); //white
            t = t.Replace("~c2", ""); //red
            t = t.Replace("~c3", ""); //cyan
            t = t.Replace("~c4", ""); //purple
            t = t.Replace("~c5", ""); //green
            t = t.Replace("~c6", ""); //blue
            t = t.Replace("~c7", ""); //yellow
            t = t.Replace("~c8", ""); //orange
            t = t.Replace("~c9", ""); //brown
            t = t.Replace("~ca", ""); //pink
            t = t.Replace("~cb", ""); //grey1
            t = t.Replace("~cc", ""); //grey2
            t = t.Replace("~cd", ""); //ltgreen
            t = t.Replace("~ce", ""); //ltblue
            t = t.Replace("~cf", ""); //grey3
            while (t.Contains("~l"))
            {
                string l = t.Substring(t.IndexOf("~l"), 3);
                string p = t.Substring(0, t.IndexOf("~l"));
                string q = "";
                if (t.IndexOf("~l") + 3 < t.Length)
                {
                    q = t.Substring(t.IndexOf("~l") + 3, t.Length - 3);
                }

                if (l[2] >= 0x30 && l[2] <= 39)
                {
                    string r = "";
                    for (int i = 0; i < (l[2] - 0x30); i++)
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
            return ".txt";
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
            return false;
        }

        public bool C64_Color()
        {
            return false;
        }
    }
}
