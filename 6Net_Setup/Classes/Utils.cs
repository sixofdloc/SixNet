using System;
namespace Net_Setup.Classes
{
    public static class Utils
    {
        public static string Input(string prompt, string defaultValue)
        {
            if (prompt != "")
            {
                var promptStr = prompt;
                if (defaultValue != "")
                {
                    promptStr = promptStr + " [" + defaultValue + "]";
                }
                Console.Write(promptStr + ": ");
            }
            var response = Console.ReadLine();
            if (response == "") response = defaultValue;
            return response;
        }

        public static void EnterToContinue()
        {
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
        }

        public static void Divider()
        {
            Console.WriteLine("===============================================================================");
        }


    }
}
