using System;
using System.Collections.Generic;

namespace Net_Console
{
    public class Menu
    {
        private readonly string Title;
        private readonly List<MenuOption> Options;
        private readonly string[] Banner;
        private int MaxMenuRows = 0;
        private int FirstMenuItem = 0;
        private int LastMenuItem = 0;

        private int currentOption;

        public Menu(string title, List<MenuOption> options) : this(title, null, options)
        {

        }

        public Menu(string[] banner, List<MenuOption> options) : this("", banner, options)
        {
        }

        public Menu(string title,string[] banner, List<MenuOption> options)
        {
            Banner = banner;
            Title = title;
            Options = options;
            currentOption = 0;
        }

        private void CalcMaxMenuRows()
        {
            var topHeight = 0;
            if (Banner != null && Banner.Length > 0)
            {
                foreach (var str in Banner)
                {
                    if (!string.IsNullOrEmpty(str)) topHeight++;
                }
            }
            if (Title != "")
            {
                topHeight++;
            }
            topHeight++;
            MaxMenuRows = Console.WindowHeight-topHeight;
        }

        private void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(80, 50);
            Console.SetWindowPosition(0, 0);
            Console.Clear();
            Console.ForegroundColor = Skin.MenuTitleColor;
            int menuOptionTopRow = 0;
            if (Banner != null && Banner.Length > 0)
            {
                foreach (var str in Banner)
                {
                    if (!string.IsNullOrEmpty(str)) Screen.PutStrCtr(str, menuOptionTopRow);
                    menuOptionTopRow++;
                }
            }
            if (Title != "")
            {
                menuOptionTopRow++;
                Screen.PutStrCtr(Title, menuOptionTopRow);
            }
            menuOptionTopRow++;
            Console.ForegroundColor = Skin.MenuOptionColor;
            for (int i = 0; i < MaxMenuRows; i++)
            {
                Console.BackgroundColor = (i == currentOption) ? Skin.MenuHiliteColor : ConsoleColor.Black;
                Screen.PutStrCtr(Options[FirstMenuItem+i].OptionText, menuOptionTopRow + i);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public int Go()
        {
            CalcMaxMenuRows();
            var menuResult = -1;
            FirstMenuItem = 0;
            LastMenuItem = Options.Count - 1;
            if (Options.Count > MaxMenuRows)
            {
                LastMenuItem = MaxMenuRows-1;
            }
            try
            {
                Console.CursorVisible = false;
                Draw();
                var menuExit = false;
                while (!menuExit)
                {
                    var keyHit = Console.ReadKey(true).Key;
                    switch (keyHit)
                    {
                        case ConsoleKey.UpArrow:
                            if (currentOption == 0 && FirstMenuItem > 0)
                            {
                                FirstMenuItem--;
                            }
                            else
                            {
                                if (currentOption > 0)
                                {
                                    currentOption--;
                                }
                            }

                            Draw();
                            break;
                        case ConsoleKey.DownArrow:
                            if (currentOption == MaxMenuRows-1 && (FirstMenuItem+currentOption < Options.Count-1))
                            {
                                FirstMenuItem++;
                            }
                            else
                            {
                                if (currentOption < MaxMenuRows-1) currentOption++;
                            }
                            Draw();
                            break;
                        case ConsoleKey.Enter:
                            menuResult = Options[FirstMenuItem+currentOption].ReturnValue;
                            menuExit = true;
                            break;
                        case ConsoleKey.Escape:
                            menuExit = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return menuResult;
        }
    }
}
