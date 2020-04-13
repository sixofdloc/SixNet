using System;
namespace Net_Console
{
    public class MenuOption
    {
        public int ReturnValue { get; set; }
        public string OptionText { get; set; }

        public MenuOption(int returnValue, string optionText)
        {
            ReturnValue = returnValue;
            OptionText = optionText;
        }
    }
}
