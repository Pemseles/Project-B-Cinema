﻿using System;
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
    class Program
    {
        private static int index = 0;
        public static int UID;
        public static int Level;
        private static void Main(string[] args)
        {
            /* Debug & Testing ************************************/
            
            ConsoleApp1.Accounts newAccount = new Accounts();
            string[] interests = new string[] { "Actie", "Romantiek", "Drama" };
            newAccount.AddAccount($"user{newAccount.GenerateID()}@mail.com", $"#{newAccount.GenerateID()}Geheim", "Pietje", "Precies", "2000-01-01", "New York WallStreet 12 2247 dc", interests);
           
            /* ./ Debug & Testing *********************************/

            List<string> LoginScreen = new List<string>() {
                "[     Inloggen     ]",
                "[    Registreren   ]",
                "[  Verder als gast ]"
            };

            Console.CursorVisible = false;
            bool LoginStartScreen = true;
            while (LoginStartScreen)
            {
                string selectedMenuItem = MainScreen(LoginScreen);
                // Login
                if (selectedMenuItem == "[     Inloggen     ]")
                {
                    ConsoleApp1.Accounts activeAccount = new Accounts();
                    UID = ConsoleApp1.Login.loginFunc();
                    Level = activeAccount.GetLevel(UID);
                
                    if (activeAccount.GetActiveStatus(UID))
                    {
                        // Account is Active
                        if(Level == 3)
                        {
                            // Account is Admin Account
                            ConsoleApp1.AdminMenu.Mainmenu();
                        } else {
                            // Default Account
                            ConsoleApp1.MainMenu.Mainmenu();
                        }
                    } else {
                        // Account is Inactive
                        // Inform user about suspended account and deny further access
                        Console.WriteLine("  Je account is (tijdelijk) opgeschort.");
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Terug");
                        ConsoleKeyInfo ckey = Console.ReadKey();
                        if (ckey.Key == ConsoleKey.Enter)
                        {
                            index = 0;
                            Console.Clear();
                        }
                    }
                }
                else if (selectedMenuItem == "[  Verder als gast ]")
                {
                    Console.Clear();
                    UID = -1;
                    ConsoleApp1.MainMenu.Mainmenu();   
                }

                // Register
                else if (selectedMenuItem == "[    Registreren   ]")
                {
                    Console.Clear();
                    ConsoleApp1.Register.register();
                }
            }
        }

        /* Frontend */
        private static string MainScreen(List<string> items)
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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            /*string logo = @"        
                         ██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗
                        ██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝
                        ██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗  
                        ██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝  
                        ╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║     ███████╗
                         ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝

           
             ";
             */

            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.Write("                                                ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.Write("                                                ");
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();
            // vragen aan PO over f11 key(fullscreen) of het disabled moet worden, zo ja vragen aan peercoach.
            if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter)
            {
                Console.Clear();
            }
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == 2)
                {
                    index = 2;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 0;
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }
    }
}