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
                var today = DateTime.Now.ToString("yyyy-MM-dd");
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
            if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter)
            {
                
            }
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index < 9)
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


        public static void ResetIndex()
        {
            index = 0;
        }

        public static void Mainmenu()
        {
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                List<string> Mainscreen = new List<string>() {
                "[    Zoek films    ]" ,
                "[     Filmlijst    ]" ,
                "[      Vandaag     ]" ,
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
                Movielist UsableList = JsonSerializer.Deserialize<Movielist>(MoviesJson);
                if (selectedMenuItem == "[    Zoek films    ]")
                {
                    Console.Clear();
               
                    // kortere reference werkt niet : argument toevoegen
                    //ConsoleApp1.SearchClass.FilmSearch();

                    //Console.WriteLine(filmLijst.FilmArray[1].Name);
                    
                    Console.Write("Geef hier op wat u zoekt :");
                    string searchClassInput = Console.ReadLine();
                    ConsoleApp1.SearchClass search1 = new ConsoleApp1.SearchClass(searchClassInput);
                    List<ConsoleApp1.Film> searchList = search1.FilmSearch(filmListAgain);
                    string searchListString = search1.FilmLengthCheck(searchList);
                    Console.WriteLine(searchListString);
                    back();

                }
                else if (selectedMenuItem == "[     Filmlijst    ]")
                {
                    ResetIndex();
                    Console.Clear();
                    FilmMenu filmListMenu = new FilmMenu();
                    Cart.UpdateFilmList(GetFilmList(false));
                    Movie selectedMovie1 = filmListMenu.Filmmenu(GetFilmList(false));
                    Console.WriteLine(selectedMovie1.Id + selectedMovie1.Moviename + selectedMovie1.Date);
                    Console.ReadLine();
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
                        Registers.moviehall();
                    }
                }
                else if (selectedMenuItem == "[Hapjes en drankjes]")
                {
                    ResetIndex();
                    Console.Clear();
                    ConsoleApp1.Products.Productmenu();
                    back();
                    // open de snacks json
                }
                else if (selectedMenuItem == "[  Informatiemenu  ]")
                {
                    ResetIndex();
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
                    ResetIndex();
                    Console.Clear();
                    Cart.PaymentScreen();
                    back();
                    // open de seats json + snackselected json
                    // if email == seats.email && email == snacksselected.email
                    // zo misschien info ophalen per account
                }
                else if (selectedMenuItem == "[       Zalen      ]")
                {
                    ResetIndex();
                    Console.Clear();
                    Registers.moviehall();
                    back();
                    Console.SetWindowSize(120, 30);
                    
                }
                else if (selectedMenuItem == "[   Mijn Account   ]")
                {
                    ResetIndex();
                    Console.Clear();
                    MyAccount User = new MyAccount(Program.UID);
                    User.OpenMenu();
                    back();
                    // Open Account Settings
                }
                else if (selectedMenuItem == "[     Uitloggen    ]")
                {
                    Console.Clear();
                    ResetIndex();
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