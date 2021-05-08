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

    class Program
    {
        private static int index = 0;



        private static void Main(string[] args)
        {

            /*
            // var options is voor het automatisch formatten van de JSON string
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            // jsontest is een object van JSONTest (functie uit Zoekfunctie.cs)
            string[] genreArr = new string[] { "Actie", "Romantiek" };
            ConsoleApp1.Film filmTest = new ConsoleApp1.Film("bee movie 16", "director henk", genreArr, 18);

            /* hier wordt jsontest omgezet tot een JSON-format string, in dit geval:
            
            {
                "Name": "bee movie 16",
                "Director": "director henk",
                "Genres": [
                    "Actie",
                    "Romantiek"
                ],
                "AgeRating": 18
            }

            *//*
            string jsonString = JsonSerializer.Serialize(filmTest, options);

            // hier wordt de JSON-format string geschreven naar deze path
            File.WriteAllText(@"C:\Users\Gebruiker\Documents\GitHub\Project-B-Cinema\Inlogscherm\JSON files\Testing.json", jsonString);
            */
            //--------------------------------------------------------------------------------------------------------------------------

            // deze lijn maakt een niewe object van class FilmArr
            //ConsoleApp1.FilmArr filmTest = new ConsoleApp1.FilmArr();
            // deze lijn opent de JSON file met alle films en maakt t een string
            //string jsonString2 = File.ReadAllText(@"C:\Users\Gebruiker\Documents\GitHub\Project-B-Cinema\Inlogscherm\JSON files\FilmList.json");
            // filepath veranderen ivm Natnael
            // deze lijn maakt van de string een object array, met elke film een individueel item in de array
            //filmTest = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonString2);
            // test om te zien of t werkt
            // kijkt in de FilmArray[positie] gevolgd door een variable in de Film class

            //--------------------------------------------------------------------------------------------------------------------------



            ConsoleApp1.Accounts user = new ConsoleApp1.Accounts();
            string[] myInterests = { "Volvo", "BMW", "Ford", "Mazda" };
            user.AddAccount("user1@email.com", "#1Geheim", "Johnny", "Bravo", "2000-01-01", "Patatstraat 12", myInterests);
            user.AddAccount("user1@email.com", "#1Geheim", "bloep", "Boga", "2000-01-01", "Patatstraat 12", myInterests);

            Console.Clear();


            /*
            string AccountPath = Path.GetFullPath(@"Accounts.json");
            string AccountsList = File.ReadAllText(AccountPath);
            ConsoleApp1.LoginArr loginData = new ConsoleApp1.LoginArr();
            loginData = JsonSerializer.Deserialize<ConsoleApp1.LoginArr>(AccountsList);
            loginData.Accounts[1].Age = "13/04/2980";
            */

            



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
                    bool login = true;
                    while (login == true)
                    {
                        Console.Clear();
                        var pass = string.Empty;
                        ConsoleKey key;
                        Console.Write("E-mail: ");      
                        string email = Console.ReadLine();
                        
                        Console.Write("Wachtwoord: "); do
                        {
                            var keyInfo = Console.ReadKey(intercept: true);
                            key = keyInfo.Key;

                            if (key == ConsoleKey.Backspace && pass.Length > 0)
                            {
                                Console.Write("\b \b");
                                pass = pass[0..^1];
                            }
                            else if (!char.IsControl(keyInfo.KeyChar))
                            {
                                Console.Write("*");
                                pass += keyInfo.KeyChar;
                            }
                        } while (key != ConsoleKey.Enter);
                        string loginemail = Console.ReadLine();
                        login = false;
                        Console.Clear();
                        ConsoleApp1.MainMenu.Mainmenu();



                    }

                }
                else if (selectedMenuItem == "[  Verder als gast ]")
                {

                        Console.Clear();
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
                    index = 0;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 2;
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