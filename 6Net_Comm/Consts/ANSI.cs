using System;
namespace Net_Comm.Consts
{
    class ANSI
    {
        public static string ESCAPE = "\x1b[";
        public static string COLOR_SET = ESCAPE + "3";
        public static string BACKGROUND_SET = ESCAPE + "4";
        public static string BRITE_COLOR_SET = ESCAPE + "1;3";
        public static string BRITE_BACKGROUND_SET = ESCAPE + "1;4";
        public static string COLOR_BLACK = COLOR_SET + "0m";
        public static string COLOR_RED = COLOR_SET + "1m";
        public static string COLOR_GREEN = COLOR_SET + "2m";
        public static string COLOR_YELLOW = COLOR_SET + "3m";
        public static string COLOR_BLUE = COLOR_SET + "4m";
        public static string COLOR_MAGENTA = COLOR_SET + "5m";
        public static string COLOR_CYAN = COLOR_SET + "6m";
        public static string COLOR_WHITE = COLOR_SET + "7m";
        public static string COLOR_BRT_BLACK = BRITE_COLOR_SET + "0m";
        public static string COLOR_BRT_RED = BRITE_COLOR_SET + "1m";
        public static string COLOR_BRT_GREEN = BRITE_COLOR_SET + "2m";
        public static string COLOR_BRT_YELLOW = BRITE_COLOR_SET + "3m";
        public static string COLOR_BRT_BLUE = BRITE_COLOR_SET + "4m";
        public static string COLOR_BRT_MAGENTA = BRITE_COLOR_SET + "5m";
        public static string COLOR_BRT_CYAN = BRITE_COLOR_SET + "6m";
        public static string COLOR_BRT_WHITE = BRITE_COLOR_SET + "7m";
        public static string BACKGROUND_BLACK = BACKGROUND_SET + "0m";
        public static string BACKGROUND_RED = BACKGROUND_SET + "1m";
        public static string BACKGROUND_GREEN = BACKGROUND_SET + "2m";
        public static string BACKGROUND_YELLOW = BACKGROUND_SET + "3m";
        public static string BACKGROUND_BLUE = BACKGROUND_SET + "4m";
        public static string BACKGROUND_MAGENTA = BACKGROUND_SET + "5m";
        public static string BACKGROUND_CYAN = BACKGROUND_SET + "6m";
        public static string BACKGROUND_WHITE = BACKGROUND_SET + "7m";
        public static string BACKGROUND_BRT_BLACK = BRITE_BACKGROUND_SET + "0m";
        public static string BACKGROUND_BRT_RED = BRITE_BACKGROUND_SET + "1m";
        public static string BACKGROUND_BRT_GREEN = BRITE_BACKGROUND_SET + "2m";
        public static string BACKGROUND_BRT_YELLOW = BRITE_BACKGROUND_SET + "3m";
        public static string BACKGROUND_BRT_BLUE = BRITE_BACKGROUND_SET + "4m";
        public static string BACKGROUND_BRT_MAGENTA = BRITE_BACKGROUND_SET + "5m";
        public static string BACKGROUND_BRT_CYAN = BRITE_BACKGROUND_SET + "6m";
        public static string BACKGROUND_BRT_WHITE = BRITE_BACKGROUND_SET + "7m";
    }
}
