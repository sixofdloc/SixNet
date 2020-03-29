using System;
using Net_Comm.Interfaces;
using Net_Logger;

namespace Net_Comm.TermTypes
{
    public class TermType_PETSCII40 : ITerminalType
    {

        public string TerminalTypeName()
        {
            return "PETSCII";
        }

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

        //private string ASCII = "\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f"
                                //+ "\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"
                                //+ " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                //+ "[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

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

        public string Horizontal_Bar()
        {
            return "\xc0";
        }


        public string Filename_Prefix()
        {
            return "Systext/";
        }

        public string Filename_Suffix()
        {
            return ".seq";
        }

        public string MCI(string originalString)
        {

            string mciTransformedString = originalString;
            try
            {
                mciTransformedString = mciTransformedString.Replace("~S1", "\x93"); //clear screen
                mciTransformedString = mciTransformedString.Replace("~S2", "\x0e"); //lowercase
                mciTransformedString = mciTransformedString.Replace("~S3", "\x8e"); //uppercase
                mciTransformedString = mciTransformedString.Replace("~S4", "\xc0"); //divider
                mciTransformedString = mciTransformedString.Replace("~G1", "\x07"); //bell
                mciTransformedString = mciTransformedString.Replace("~C0", "\x90"); //black
                mciTransformedString = mciTransformedString.Replace("~C1", "\x05"); //white
                mciTransformedString = mciTransformedString.Replace("~C2", "\x1c"); //red
                mciTransformedString = mciTransformedString.Replace("~C3", "\x9f"); //cyan
                mciTransformedString = mciTransformedString.Replace("~C4", "\x9c"); //purple
                mciTransformedString = mciTransformedString.Replace("~C5", "\x1e"); //green
                mciTransformedString = mciTransformedString.Replace("~C6", "\x1f"); //blue
                mciTransformedString = mciTransformedString.Replace("~C7", '\x9e'.ToString()); //yellow
                mciTransformedString = mciTransformedString.Replace("~C8", "\x81"); //orange
                mciTransformedString = mciTransformedString.Replace("~C9", "\x95"); //brown
                mciTransformedString = mciTransformedString.Replace("~CA", "\x96"); //pink
                mciTransformedString = mciTransformedString.Replace("~CB", "\x97"); //grey1
                mciTransformedString = mciTransformedString.Replace("~CC", "\x98"); //grey2
                mciTransformedString = mciTransformedString.Replace("~CD", "\x99"); //ltgreen
                mciTransformedString = mciTransformedString.Replace("~CE", "\x9a"); //ltblue
                mciTransformedString = mciTransformedString.Replace("~CF", "\x9b"); //grey3
                mciTransformedString = mciTransformedString.Replace("~D0", "\x92"); //black
                mciTransformedString = mciTransformedString.Replace("~D1", "\x12\x05"); //white
                mciTransformedString = mciTransformedString.Replace("~D2", "\x12\x1c"); //red
                mciTransformedString = mciTransformedString.Replace("~D3", "\x12\x9f"); //cyan
                mciTransformedString = mciTransformedString.Replace("~D4", "\x12\x9c"); //purple
                mciTransformedString = mciTransformedString.Replace("~D5", "\x12\x1e"); //green
                mciTransformedString = mciTransformedString.Replace("~D6", "\x12\x1f"); //blue
                mciTransformedString = mciTransformedString.Replace("~D7", "\x12" + '\x9e'.ToString()); //yellow
                mciTransformedString = mciTransformedString.Replace("~D8", "\x12\x81"); //orange
                mciTransformedString = mciTransformedString.Replace("~D9", "\x12\x95"); //brown
                mciTransformedString = mciTransformedString.Replace("~DA", "\x12\x96"); //pink
                mciTransformedString = mciTransformedString.Replace("~DB", "\x12\x97"); //grey1
                mciTransformedString = mciTransformedString.Replace("~DC", "\x12\x98"); //grey2
                mciTransformedString = mciTransformedString.Replace("~DD", "\x12\x99"); //ltgreen
                mciTransformedString = mciTransformedString.Replace("~DE", "\x12\x9a"); //ltblue
                mciTransformedString = mciTransformedString.Replace("~DF", "\x12\x9b"); //grey3
                while (mciTransformedString.Contains("~L"))
                {
                    string MCILCommand = mciTransformedString.Substring(mciTransformedString.IndexOf("~L"), 3);
                    string everythingBeforeMCIL = mciTransformedString.Substring(0, mciTransformedString.IndexOf("~L"));
                    string q = "";
                    int positionOfMCILPreamble = mciTransformedString.IndexOf("~L");
                    if (positionOfMCILPreamble + 3 < mciTransformedString.Length)
                    {
                        q = mciTransformedString.Substring(positionOfMCILPreamble + 3, mciTransformedString.Length - (positionOfMCILPreamble + 3));
                    }

                    if (MCILCommand[2] >= '0' && MCILCommand[2] <= '9')
                    {
                        string r = "";
                        for (int i = 0; i < (MCILCommand[2] - 0x30); i++)
                        {
                            r = r + CRLF();
                        }
                        mciTransformedString = everythingBeforeMCIL + r + q;
                    }
                    else
                    {
                        mciTransformedString = everythingBeforeMCIL + q;
                    }
                }
            }
            catch (Exception exception)
            {
                mciTransformedString = originalString;
                LoggingAPI.Exception(exception, new {originalString,mciTransformedString });
            }
            return mciTransformedString;
        }

        public string TranslateToTerminal(string stringToTranslate)
        {
            string t = "";
            foreach (char c in stringToTranslate)
            {
                char d = c;
                if ((byte)d <= 0x7e)
                {
                    try
                    {
                        d = PETSCII[d];
                    } catch(Exception)
                    {
                        d = c;
                    }
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
