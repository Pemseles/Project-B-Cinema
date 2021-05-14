using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using static ConsoleApp1.MainMenu;
using static ConsoleApp1.Registers;
// restricties opzetten voor gebruik van alleen backspace en enter anders redrawd ie m 
namespace ConsoleApp1
{
    public class MainMenu
    {
        private static int index = 0;

            private static string MainScreen(List<string> items)
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
                if (index == 8)
                {
                    index = 0;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 8;
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
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                List<string> Mainscreen = new List<string>() {
                "[     Zoek films   ]" ,
                "[     Alle films   ]" ,
                "[Hapjes en drankjes]" ,
                "[  Informatiemenu  ]" ,
                "[    Review menu   ]" ,
                "[    Winkelmand    ]" ,
                "[    VIP pagina    ]" ,
                "[       Zalen      ]" ,
                "[     Uitloggen    ]"
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);
                if (selectedMenuItem == "[     Zoek films   ]")
                {
                    Console.Clear();
               
                    // kortere reference werkt niet : argument toevoegen
                    //ConsoleApp1.SearchClass.FilmSearch();
                    string filmJSONPath = Path.GetFullPath(@"FilmList.json");
                    string jsonStringFilmList = File.ReadAllText(filmJSONPath);

                    ConsoleApp1.FilmArr filmList = new ConsoleApp1.FilmArr();
                    filmList = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmList);
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
                    Console.WriteLine("Dit is de filmlijst");
                    back();
                    // open de movies json
                }
                else if (selectedMenuItem == "[Hapjes en drankjes]")
                {
                    Console.Clear();
                    ConsoleApp1.Products.Productmenu();
                    back();
                    // open de snacks json
                }
                else if (selectedMenuItem == "[  Informatiemenu  ]")
                {
                    Console.Clear();
                    Console.WriteLine("Dit is het informatie menu");
                    back();
                    // open de info panels
                }
                else if (selectedMenuItem == "[    Review menu   ]")
                {
                    Console.Clear();
                    Console.WriteLine("Dit is het review menu");
                    back();
                    // open de review list of json
                }
                else if (selectedMenuItem == "[    Winkelmand    ]")
                {
                    Console.Clear();
                    // open de seats json + snackselected json
                    // if email == seats.email && email == snacksselected.email
                    // zo misschien info ophalen per account
                }
                else if (selectedMenuItem == "[    VIP pagina    ]")
                {
                    Console.Clear();
                    Console.WriteLine("dit is de VIP pagina");
                    back();
                    // if account registratie == vip 
                    // kan dit geopend worden 
                }
                else if (selectedMenuItem == "[       Zalen      ]")
                {
                    Console.Clear();
                    Registers.moviehall();
                    back();
                    Console.SetWindowSize(120, 30);
                    // if account registratie == vip 
                    // kan dit geopend worden 
                }
                else if (selectedMenuItem == "[     Uitloggen    ]")
                {
                    Console.Clear();
                    mainmenubool = false;   
                }
                Console.Clear();
            }
            
        }
    }
}