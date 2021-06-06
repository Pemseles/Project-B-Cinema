using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
// using consoleapp1.mapnaam.filenaam;
using static ConsoleApp1.MainMenu;
using static ConsoleApp1.Film;
using static ConsoleApp1.Accounts;


namespace ConsoleApp1
{
    class Program : MenuController
    {
        public static int UID;
        public static int Level;
        private static void Main(string[] args)
        {
            Debug.Run2();
            Console.SetWindowSize(120, 30);
            Console.CursorVisible = false;
            List<string> LoginScreen = new List<string>() {
                "[     Inloggen     ]",
                "[    Registreren   ]",
                "[  Verder als gast ]"
            };

            bool LoginStartScreen = true;
            while (LoginStartScreen)
            { 
                Logo.Print();
                string selectedMenuItem = MainScreen(LoginScreen);
                // Login
                if (selectedMenuItem == "[     Inloggen     ]")
                {
                    ResetIndex();
                    ConsoleApp1.Accounts activeAccount = new Accounts();
                    UID = ConsoleApp1.Login.loginFunc();
                    Level = activeAccount.GetLevel(UID);

                    if (activeAccount.GetActiveStatus(UID))
                    {
                        // Account is Active
                        if (Level == 3)
                        {
                            // Account is Admin Account
                            ConsoleApp1.AdminMenu.Mainmenu();
                        }
                        else
                        {
                            // Default Account
                            ConsoleApp1.MainMenu.Mainmenu();
                        }
                    }
                    else
                    {
                        // Account is Inactive
                        // Inform user about suspended account and deny further access
                        Console.WriteLine("  Je account is (tijdelijk) opgeschort.");
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("[     Terug     ]");
                        ConsoleKeyInfo ckey = Console.ReadKey();
                        if (ckey.Key == ConsoleKey.Enter)
                        {
                            ResetIndex();
                            Console.Clear();
                        }
                    }
                }
                else if (selectedMenuItem == "[  Verder als gast ]")
                {
                    ResetIndex();
                    Console.Clear();
                    UID = -1;
                    ConsoleApp1.Guest.Mainmenu();
                }

                // Register
                else if (selectedMenuItem == "[    Registreren   ]")
                {
                    ResetIndex();
                    Console.Clear();
                    ConsoleApp1.Register.register();
                }
            }
        }
    }

    class Logo
    {
        public static void Print()
        {
            Console.Write("                         ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║     ███████╗");
            Console.ResetColor();
            Console.Write("                         ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝");
            Console.ResetColor();
            Console.WriteLine("\n\n");
           

            /*string logo = @"        
                         ██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗
                        ██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝
                        ██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗  
                        ██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝  
                        ╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║     ███████╗
                         ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝

                       
             ";
                                 Credits: Avienda, Christ, Jaring, Sergio & Thijmen.
            */
            
        }
    }

    class Debug // TIJDELIJK 
    {
        public static void Run()
        {
            // :::::::: PUT YOUR DEBUG CODE IN HERE :::::::: \\

            ConsoleApp1.Accounts newAccount = new Accounts();
            string[] interests = new string[] { "Actie", "Romantiek", "Drama" };
            newAccount.AddAccount($"user{newAccount.GenerateID()}@mail.com", $"#{newAccount.GenerateID()}Geheim", "Pietje", "Precies", "2000-01-01", "New York WallStreet 12 2247 dc", interests);
        }
        public static void Run2()
        {
            // :::::::: PUT YOUR DEBUG CODE IN HERE :::::::: \\

            ConsoleApp1.ReviewCreation review = new ReviewCreation();
            ReviewCreation.AddReview(2, 1, 3, "John Doe", "The Titanic", "Mooie film, wel lang", 3, "2021-06-04-17:25");
        }
    }
}