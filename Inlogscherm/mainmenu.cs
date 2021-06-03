using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using static ConsoleApp1.MainMenu;
using static ConsoleApp1.Registers;
using static ConsoleApp1.Infopanels;
// restricties opzetten voor gebruik van alleen backspace en enter anders redrawd ie m 
namespace ConsoleApp1
{
    public class MainMenu
    {
        private static int index = 0;
        public static Checkout Cart = new Checkout();
        public static string MoviesJson = File.ReadAllText(Path.GetFullPath(@"movies.json"));
        public static string filmJSONPath = Path.GetFullPath(@"FilmList.json");
        public static string jsonStringFilmList = File.ReadAllText(filmJSONPath);
        public static FilmArr filmList = new FilmArr();
        public static FilmArr filmListAgain = JsonSerializer.Deserialize<FilmArr>(jsonStringFilmList);
        public static List<Movie> GetFilmList(bool todayDate)
        {
            Movielist UsableList = new Movielist() { };
            UsableList = JsonSerializer.Deserialize<Movielist>(MoviesJson);
            List<Movie> filmList = new List<Movie> { };
            if (!todayDate) {
                foreach (var filmItem in UsableList.Movies)
                {
                    filmList.Add(filmItem);
                }
            }
            else
            {
                // haalt de datum op voor vandaag
                //var today = DateTime.Now.ToString("yyyy-MM-dd");
                var today = "2021-04-01";
                foreach (var filmItem in UsableList.Movies)
                {
                    for (int i = 0; i < UsableList.Movies.Length; i++)
                    {
                        if (UsableList.Movies[i].Date == today)
                        {
                            filmList.Add(filmItem);
                        }
                    }
                }
            }
            return filmList;
        }
        public static string MainScreen(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("                                                ");
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index < items.Count-1)
                {
                    index++;
                }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index > 0)
                {
                    index--;
                }
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
        public static void back()
        {
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.Backspace)
            {
                Console.Clear();
                Mainmenu();
            } 
        }


        public static void resetIndex()
        {
            index = 0;
        }

        public static void Mainmenu()
        {
            string filmJSONPath = Path.GetFullPath(@"FilmList.json");
            string jsonStringFilmList = File.ReadAllText(filmJSONPath);
            filmList = new ConsoleApp1.FilmArr();
            filmList = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmList);
            Checkout Cart = new Checkout();
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                List<string> Mainscreen = new List<string>() {
                "[       Films      ]" ,
                "[Hapjes en drankjes]" ,
                "[  Informatiemenu  ]" ,
                "[    Review menu   ]" ,
                "[   Winkelmandje   ]" ,
                "[       Zalen      ]" ,
                "[   Mijn Account   ]" ,
                "[     Uitloggen    ]",
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);
                if (selectedMenuItem == "[       Films      ]")
                {
                    Console.Clear();
                    FilmMenu filmListMenu = new FilmMenu();
                    Cart.UpdateFilmList(GetFilmList(false));
                    Movie selectedMovie1 = filmListMenu.Filmmenu(GetFilmList(false));
                }
                else if (selectedMenuItem == "[      Vandaag     ]")
                {
                    ResetIndex();
                    Console.Clear();
                    //var today = DateTime.Now.ToString("yyyy-MM-dd");
                    FilmMenu filmListToday = new FilmMenu();
                    Cart.UpdateFilmList(GetFilmList(true));
                    Movie selectedMovie = filmListToday.Filmmenu(GetFilmList(true));
                    
                    if (selectedMovie != null)
                    {
                        Registers.Moviehall();
                    }
                }
                else if (selectedMenuItem == "[Hapjes en drankjes]")
                {
                    resetIndex();
                    Console.Clear();
                    ConsoleApp1.Products.Productmenu();
                    back();
                    // open de snacks json
                }
                else if (selectedMenuItem == "[  Informatiemenu  ]")
                {
                    resetIndex();
                    Console.Clear();
                    Infopanels panelObj = new Infopanels();
                    panelObj.InfoScreen();
                    back();
                    // open de info panels
                }
                else if (selectedMenuItem == "[    Review menu   ]")
                {
                    Console.Clear();
                    Review.Revmenu();
                    back();
                    // open de review list of json
                }
                else if (selectedMenuItem == "[   Winkelmandje   ]")
                {
                    resetIndex();
                    Console.Clear();
                    Cart.PaymentScreen();
                    back();
                    // open de seats json + snackselected json
                    // if email == seats.email && email == snacksselected.email
                    // zo misschien info ophalen per account
                }
                else if (selectedMenuItem == "[       Zalen      ]")
                {
                    resetIndex();
                    Console.Clear();
                    Registers.Moviehall();
                    back();
                    Console.SetWindowSize(120, 30);
                    
                }
                else if (selectedMenuItem == "[   Mijn Account   ]")
                {
                    resetIndex();
                    Console.Clear();
                    MyAccount User = new MyAccount(Program.UID);
                    User.OpenMenu();
                    back();
                    // Open Account Settings
                }
                else if (selectedMenuItem == "[     Uitloggen    ]")
                {
                    Console.Clear();
                    resetIndex();
                    mainmenubool = false;
                    /*
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                    */ 
                }
                Console.Clear();
            }
            
        }
    }
}