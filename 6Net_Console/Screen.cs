using System;
namespace Net_Console
{
    public static class Screen
    {

        public static void PutStr(string text, int x, int y)
        {
            try
            {
                if (x < 0) x = 0;
                if (x > Console.WindowWidth - 1) x = Console.WindowWidth - 1;
                if (y < 0) y = 0;
                if (y > Console.WindowHeight - 1) y = Console.WindowHeight - 1;
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void PutStrCtr(string text, int y)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - (text.Length / 2), y);
            Console.Write(text);
        }

    }
}