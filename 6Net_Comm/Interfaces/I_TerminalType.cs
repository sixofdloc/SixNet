using System;
namespace Net_Comm.Interfaces
{
    public interface I_TerminalType
    {
        string CRLF();
        char BACKSPACE();
        char Input_Terminator();
        string Filename_Prefix();
        string Filename_Suffix();
        int Columns();
        bool Linefeeds();
        bool ANSI_Color();
        bool C64_Color();

        string TerminalTypeName();


        string MCI(String s); //Expects string to be pretranslated
        string TranlateToTerminal(string s); //In - ASCII, Out - translated to terminalType
        char TranslateToTerminal(char c);
        char TranslateFromTerminal(char c);

    }
}
