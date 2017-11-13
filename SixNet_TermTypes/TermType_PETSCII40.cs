using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_TermTypes
{
    class TermType_PETSCII40 : I_TerminalType
    {
        private string PETSCII = "\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f"
                                + "\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"
                                + " !\"#$%&'()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz"
                                + "[\\]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~"
                                        + "\x80\x81\x82\x83\x84\x85\x86\x87\x88\x89\x8a\x8b\x8c\x8d\x8e\x8f"       //0x80-0x8F
                                + "\x90\x91\x92\x93\x94\x95\x96\x97\x98\x99\x9a\x9b\x9c\x9d\x9e\x9f"       //0x90-0x9F
                                + "\xa0\xa1\xa2\xa3\xa4\xa5\xa6\xa7\xa8\xa9\xaa\xab\xac\xad\xae\xaf"       //0xA0-0xAF
                                + "\xb0\xb1\xb2\xb3\xb4\xb5\xb6\xb7\xb8\xb9\xba\xbb\xbc\xbd\xbe\xbf"       //0xB0-0xBF
                                + "\xc0\xc1\xc2\xc3\xc4\xc5\xc6\xc7\xc8\xc9\xca\xcb\xcc\xcd\xce\xcf"       //0xC0-0xCF
                                + "\xd0\xd1\xd2\xd3\xd4\xd5\xd6\xd7\xd8\xd9\xda\xdb\xdc\xdd\xde\xdf"       //0xD0-0xDF
                                + "\xe0\xe1\xe2\xe3\xe4\xe5\xe6\xe7\xe8\xe9\xea\xeb\xec\xed\xee\xef"       //0xE0-0xEF
                                + "\xf0\xf1\xf2\xf3\xf4\xf5\xf6\xf7\xf8\xf9\xfa\xfb\xfc\xfd\xfe\xff";      //0xF0-0xFF

        private string ASCII = "\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f"
                                + "\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"
                                + " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                + "[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        /*       private string PETSCII = "\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f"
                                + "\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"
                                + " !\"#$%&'()*+,-./"
                                + "0123456789:;<=>?"
                                + "@abcdefghijklmno"
                                + "pqrstuvwxyz[\\]^_"
                                + "\x60ABCDEFGHIJKLMNO"
                                + "PQRSTUVWXYZ{|}~\x7f"
                                + "\x80\x81\x82\x83\x84\x85\x86\x87\x88\x89\x8a\x8b\x8c\x8d\x8e\x8f"       //0x80-0x8F
                                + "\x90\x91\x92\x93\x94\x95\x96\x97\x98\x99\x9a\x9b\x9c\x9d\x9e\x9f"       //0x90-0x9F
                                + "\xa0\xa1\xa2\xa3\xa4\xa5\xa6\xa7\xa8\xa9\xaa\xab\xac\xad\xae\xaf"       //0xA0-0xAF
                                + "\xb0\xb1\xb2\xb3\xb4\xb5\xb6\xb7\xb8\xb9\xba\xbb\xbc\xbd\xbe\xbf"       //0xB0-0xBF
                                + "0x60ABCDEFGHIJKLMNO"
                                + "PQRSTUVWXYZ{|}~\xdf"
                                + "\xe0\xe1\xe2\xe3\xe4\xe5\xe6\xe7\xe8\xe9\xea\xeb\xec\xed\xee\xef"       //0xE0-0xEF
                                + "\xf0\xf1\xf2\xf3\xf4\xf5\xf6\xf7\xf8\xf9\xfa\xfb\xfc\xfd\xfe\xff";      //0xF0-0xFF

        private string ASCII = "\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f"       //0x00-0x0f
                                + "\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"    //0x10-0x1f
                                + " !\"#$%&'()*+,-./"                                                   //0x20-0x2f
                                + "0123456789:;<=>?"                                                    //0x30-0x3f
                                + "@ABCDEFGHIJKLMNO"                                                    //0x40-0x4f
                                + "PQRSTUVWXYZ[\\]^_"                                                   //0x50-0x5f
                                + "`abcdefghijklmno"                                                    //0x60-0x6f
                                + "pqrstuvwxyz{|}~\x7f"                                                 //0x70-0x7f
                                + "\x80\x81\x82\x83\x84\x85\x86\x87\x88\x89\x8a\x8b\x8c\x8d\x8e\x8f"       //0x80-0x8F
                                + "\x90\x91\x92\x93\x94\x95\x96\x97\x98\x99\x9a\x9b\x9c\x9d\x9e\x9f"       //0x90-0x9F
                                + "\xa0\xa1\xa2\xa3\xa4\xa5\xa6\xa7\xa8\xa9\xaa\xab\xac\xad\xae\xaf"       //0xA0-0xAF
                                + "\xb0\xb1\xb2\xb3\xb4\xb5\xb6\xb7\xb8\xb9\xba\xbb\xbc\xbd\xbe\xbf"       //0xB0-0xBF
                                + "\xc0\xc1\xc2\xc3\xc4\xc5\xc6\xc7\xc8\xc9\xca\xcb\xcc\xcd\xce\xcf"       //0xC0-0xCF
                                + "\xd0\xd1\xd2\xd3\xd4\xd5\xd6\xd7\xd8\xd9\xda\xdb\xdc\xdd\xde\xdf"       //0xD0-0xDF
                                + "\xe0\xe1\xe2\xe3\xe4\xe5\xe6\xe7\xe8\xe9\xea\xeb\xec\xed\xee\xef"       //0xE0-0xEF
                                + "\xf0\xf1\xf2\xf3\xf4\xf5\xf6\xf7\xf8\xf9\xfa\xfb\xfc\xfd\xfe\xff";      //0xF0-0xFF
*/
        public string CRLF()
        {
            return ((char)0x0d).ToString();
        }

        public char BACKSPACE()
        {
            return (char)0x14;
        }

        public char Input_Terminator()
        {
            return (char)0x0d;
        }


        public string Filename_Prefix()
        {
            return "Systext\\";
        }

        public string Filename_Suffix()
        {
            return ".seq";
        }

        public string MCI(string s)
        {
            
            string t = s;
            try
            {
                t = t.Replace("~S1", "\x93"); //clear screen
                t = t.Replace("~S2", "\x0e"); //lowercase
                t = t.Replace("~S3", "\x8e"); //uppercase
                t = t.Replace("~S4", "\xc0"); //divider
                t = t.Replace("~G1", "\x07"); //bell
                t = t.Replace("~C0", "\x90"); //black
                t = t.Replace("~C1", "\x05"); //white
                t = t.Replace("~C2", "\x1c"); //red
                t = t.Replace("~C3", "\x9f"); //cyan
                t = t.Replace("~C4", "\x9c"); //purple
                t = t.Replace("~C5", "\x1e"); //green
                t = t.Replace("~C6", "\x1f"); //blue
                t = t.Replace("~C7", '\x9e'.ToString()); //yellow
                t = t.Replace("~C8", "\x81"); //orange
                t = t.Replace("~C9", "\x95"); //brown
                t = t.Replace("~CA", "\x96"); //pink
                t = t.Replace("~CB", "\x97"); //grey1
                t = t.Replace("~CC", "\x98"); //grey2
                t = t.Replace("~CD", "\x99"); //ltgreen
                t = t.Replace("~CE", "\x9a"); //ltblue
                t = t.Replace("~CF", "\x9b"); //grey3
                t = t.Replace("~D0", "\x92"); //black
                t = t.Replace("~D1", "\x12\x05"); //white
                t = t.Replace("~D2", "\x12\x1c"); //red
                t = t.Replace("~D3", "\x12\x9f"); //cyan
                t = t.Replace("~D4", "\x12\x9c"); //purple
                t = t.Replace("~D5", "\x12\x1e"); //green
                t = t.Replace("~D6", "\x12\x1f"); //blue
                t = t.Replace("~D7", "\x12" + '\x9e'.ToString()); //yellow
                t = t.Replace("~D8", "\x12\x81"); //orange
                t = t.Replace("~D9", "\x12\x95"); //brown
                t = t.Replace("~DA", "\x12\x96"); //pink
                t = t.Replace("~DB", "\x12\x97"); //grey1
                t = t.Replace("~DC", "\x12\x98"); //grey2
                t = t.Replace("~DD", "\x12\x99"); //ltgreen
                t = t.Replace("~DE", "\x12\x9a"); //ltblue
                t = t.Replace("~DF", "\x12\x9b"); //grey3
                while (t.Contains("~L"))
                {
                    string l = t.Substring(t.IndexOf("~L"), 3);
                    string p = t.Substring(0, t.IndexOf("~L"));
                    string q = "";
                    int x = t.IndexOf("~L");
                    if (x + 3 < t.Length)
                    {
                        q = t.Substring(x + 3, t.Length - (x + 3));
                    }

                    if (l[2] >= '0' && l[2] <= '9')
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
            }
            catch (Exception e)
            {
                t = s;
            }
            return t;
        }

        public string TranlateToTerminal(string s)
        {
            string t = "";
            foreach (char c in s)
            {
                char d = c;
                if ((byte)d < 0x7e )
                {
                    d = PETSCII[d];
                }
                t = t + d;
            }
            return t;
        }

        public char TranslateToTerminal(char c)
        {
            return PETSCII[c];
        }

        public char TranslateFromTerminal(char c)
        {
            if (c >= 0xc0 && c <= 0xdf) c = (char)(c - 0x60);
            return PETSCII[c];
        }


        public int Columns()
        {
            return 40;
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
            return true;
        }
    }
}
