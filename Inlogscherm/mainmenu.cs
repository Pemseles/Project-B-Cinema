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
        public static FilmArr filmList;

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
                if (selectedMenuItem == "[    Zoek films    ]")
                {
                    Console.Clear();
               
                    // kortere reference werkt niet : argument toevoegen
                    //ConsoleApp1.SearchClass.FilmSearch();

                    //Console.WriteLine(filmLijst.FilmArray[1].Name);
                    
                    Console.Write("Geef hier op wat u zoekt :");
                    string searchClassInput = Console.ReadLine();
                    ConsoleApp1.SearchClass search1 = new ConsoleApp1.SearchClass(searchClassInput);
                    List<ConsoleApp1.Film> searchList = search1.FilmSearch(filmList);
                    string searchListString = search1.FilmLengthCheck(searchList);
                    Console.WriteLine(searchListString);
                    back();

                }
                else if (selectedMenuItem == "[     Filmlijst    ]")
                {
                    resetIndex();
                    Console.Clear();

                    string moviesjson = File.ReadAllText(Path.GetFullPath(@"movies.json"));
                    var movielist = JsonSerializer.Deserialize<Movielist>(moviesjson);
                    string[] FilmList = new string[movielist.movies.Length];
                    for (int i = 0; i < movielist.movies.Length; i++)
                    {
                        FilmList[i] = movielist.movies[i].moviename;
                    }
                    var selectedmovie = filmmenu.Filmmenu(FilmList);
                    Console.WriteLine(selectedmovie.id + selectedmovie.moviename + selectedmovie.date);
                    Console.ReadLine();
                }
                else if (selectedMenuItem == "[      Vandaag     ]")
                {
                    resetIndex();
                    Console.Clear();

                    string moviesjson = File.ReadAllText(Path.GetFullPath(@"movies.json"));
                    var movielist = JsonSerializer.Deserialize<Movielist>(moviesjson);
                    //var today = DateTime.Now.ToString("yyyy-MM-dd");
                    var today = "2021-04-01";
                    var todaystring = "";
                    for (int i = 0;i<movielist.movies.Length;i++)
                    {
                        if (movielist.movies[i].date == today)
                        {
                            todaystring += movielist.movies[i].moviename+"###";
                        }
                    }
                    string[] todaylist = todaystring.Split("###");
                    var selectedmovie = filmmenu.Filmmenu(todaylist, "ja");
                    Registers.moviehall();
<<<<<<< HEAD
                    
=======
>>>>>>> parent of 57aff22 (Voortgang Checkout)

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
                    Registers.moviehall();
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