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
                "[Hapjes en drankjes]" ,
                "[  Informatiemenu  ]" ,
                "[    Review menu   ]" ,
                "[   Winkelmandje   ]" ,
                "[    VIP pagina    ]" ,
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
                if (selectedMenuItem == "[     Alle films   ]")
                {
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
                else if (selectedMenuItem == "[Hapjes en drankjes]")
                {
                    Console.Clear();
                    ConsoleApp1.Products.Productmenu();
                    back();
                    // open de snacks json
                }
                if (selectedMenuItem == "[  Informatiemenu  ]")
                {
                    Console.Clear();
                    Infopanels panelObj = new Infopanels();
                    panelObj.InfoScreen();
                    back();
                    // open de info panels
                }
                if (selectedMenuItem == "[    Review menu   ]")
                {
                    Console.Clear();
                    Review.Revmenu();
                    back();
                    // open de review list of json
                }
                if (selectedMenuItem == "[   Winkelmandje   ]")
                {
                    Console.Clear();
                    Cart.PaymentScreen();
                    back();
                    // open de seats json + snackselected json
                    // if email == seats.email && email == snacksselected.email
                    // zo misschien info ophalen per account
                }
                if (selectedMenuItem == "[    VIP pagina    ]")
                {
                    Console.Clear();
                    Console.WriteLine("dit is de VIP pagina");
                    back();
                    // if account registratie == vip 
                    // kan dit geopend worden 
                }
                if (selectedMenuItem == "[       Zalen      ]")
                {
                    Console.Clear();
                    Registers.moviehall();
                    back();
                    Console.SetWindowSize(120, 30);
                    
                }
                else if (selectedMenuItem == "[   Mijn Account   ]")
                {
                    Console.Clear();
                    MyAccount User = new MyAccount(Program.UID);
                    User.OpenMenu();
                    back();
                    // Open Account Settings
                }
                else if (selectedMenuItem == "[     Uitloggen    ]")
                {
                    Console.Clear();

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