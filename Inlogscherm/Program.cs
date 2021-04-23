﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;


// using consoleapp1.mapnaam.filenaam;
using static ConsoleApp1.Film;
using static ConsoleApp1.Accounts;


namespace Console_Menu
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

            string filmJSONPath = Path.GetFullPath(@"FilmList.json");
            Console.WriteLine(filmJSONPath);
            string jsonStringFilmLijst = File.ReadAllText(filmJSONPath);

            ConsoleApp1.FilmArr filmLijst = new ConsoleApp1.FilmArr();
            filmLijst = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmLijst);
            Console.WriteLine(filmLijst.FilmArray[1].Name);

            Console.WriteLine("Please enter thing. ");
            string inputZoekfunctie = Console.ReadLine();
            ConsoleApp1.Zoekfunctie zoeken = new ConsoleApp1.Zoekfunctie(inputZoekfunctie);
            zoeken.ZoekMethod();
            Console.WriteLine(zoeken.InputZoek);



            ConsoleApp1.Accounts user = new ConsoleApp1.Accounts();
            string[] myInterests = { "Volvo", "BMW", "Ford", "Mazda" };
            user.AddAccount("user1@email.com", "#1Geheim", "Johnny", "Bravo", "2000-01-01", "Patatstraat 12", myInterests);
            user.AddAccount("user1@email.com", "#1Geheim", "bloep", "Boga", "2000-01-01", "Patatstraat 12", myInterests);




            /*
            string AccountPath = Path.GetFullPath(@"Accounts.json");
            string AccountsList = File.ReadAllText(AccountPath);
            ConsoleApp1.LoginArr loginData = new ConsoleApp1.LoginArr();
            loginData = JsonSerializer.Deserialize<ConsoleApp1.LoginArr>(AccountsList);
            loginData.Accounts[1].Age = "13/04/2980";

            */








            List<string> LoginScreen = new List<string>() {
                "[     Inloggen    ]",
                "[    Registreren   ]",
                "[  Verder als gast ]"
            };

            List<string> movieList = new List<string>() {
                "Titanic                                            - James Cameron               09:00-10:30          zaal 1       2D",
                "Avatar                                             - James Cameron               09:15-10:45          zaal 2       4D",
                "Ready Player One                                   - Steven Spielberg            10.25-11:55          zaal 3       3D",
                "Pulp Fiction                                       - Quentin Tarantino           11:20-12:50          zaal 4       DOLBY",
                "Interstellar                                       - Christopher Nolan           12:50-14:20          zaal 5       IMAX",
                "the lord of the rings the return of the king       - Peter Jackson               13:45-15:15          zaal 2       3D",
                "The Notebook                                       - Nick Cassavetes             15:40-17:10          zaal 1       IMAX",
                "Joker                                              - Todd Phillips               16:00-17:30          zaal 3       4D",
                "The Wolf of Wallstreet                             - Martin Scorsese             17:15-18:45          zaal 5       3D"
            };       
            Console.CursorVisible = false;
            bool LoginStartScreen = true;
            while (LoginStartScreen)
            {
                string selectedMenuItem = MainScreen(LoginScreen);
                // Login
                if (selectedMenuItem == "[     Inloggen    ]")
                {
                    bool login = true;
                    while (login == true) {
                        Console.Clear();
                        var pass = string.Empty;
                        ConsoleKey key;
                        Console.Write("E-mail: "); Console.ReadLine();
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

                        login = false;
                        Console.Clear();
                        LoginStartScreen = false;
                        for (int i = 0; i < movieList.Count; i++)
                        {
                            Console.WriteLine(movieList[i]);
                            Console.WriteLine();
                        }
                        ConsoleKeyInfo ckey = Console.ReadKey();
                        if (ckey.Key == ConsoleKey.Enter)
                        {
                            Environment.Exit(0);
                        }


                    }

                }
                else if (selectedMenuItem == "[  Verder als gast ]")
                {

                    Console.Clear();
                    LoginStartScreen = false;
                    for (int i = 0; i < movieList.Count; i++)
                    {
                        Console.WriteLine(movieList[i]);
                        Console.WriteLine();
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                    
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
                    Console.Write("                                                        ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    
                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.Write("                                                        ");
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

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