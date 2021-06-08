using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using static ConsoleApp1.MainMenu;
using static ConsoleApp1.Theaterhall;
using static ConsoleApp1.Infopanels;
// restricties opzetten voor gebruik van alleen backspace en enter anders redrawd ie m 
namespace ConsoleApp1
{   
    public abstract class MenuController // Abstract Class
    {
        private static int index = 0;
        public static string MainScreen(List<string> items)
        {
            Console.CursorVisible = false;
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
                if (index < items.Count - 1)
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
                Console.Clear();
                return items[index];
            }
            else
            {
                Console.Clear();
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
                if (Program.Level >= 3) { 
                    AdminMenu.Mainmenu();
                } else {
                    Mainmenu();
                }
            }
        }

        public static void ResetIndex()
        {
            index = 0;
        }
    }
    public class MainMenu : MenuController  // Inherits from Abstract Class
    {
        public static Checkout Cart = new Checkout();
        public static string MoviesJson = File.ReadAllText(Path.GetFullPath(@"movies.json"));
        public static string filmJSONPath = Path.GetFullPath(@"FilmList.json");
        public static string jsonStringFilmList = File.ReadAllText(filmJSONPath);
        public static FilmArr filmList = new FilmArr();
        public static FilmArr filmListAgain = JsonSerializer.Deserialize<FilmArr>(jsonStringFilmList);
        public static string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
        public static Filmsuperclass movielist = JsonSerializer.Deserialize<Filmsuperclass>(moviesjson);
        public static string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.json"));
        public static Filmsuperclass movieinstancelist = JsonSerializer.Deserialize<Filmsuperclass>(movieinstancesjson);

        public static void Mainmenu()
        {
            string filmJSONPath = Path.GetFullPath(@"FilmList.json");
            string jsonStringFilmList = File.ReadAllText(filmJSONPath);
            var filmList = new ConsoleApp1.FilmArr();
            filmList = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmList);
            Filmlist films = new Filmlist();
            Filminstance selectedinstance = new Filminstance();
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();

                List<string> Mainscreen = new List<string>() {
                "[       Films      ]" ,
                "[Hapjes en drankjes]" ,
                "[  Informatiemenu  ]" ,
                "[    Reviewmenu    ]" ,
                "[   Winkelmandje   ]" ,
                "[       Zalen      ]" ,
                "[   Mijn Account   ]" ,
                "[     Uitloggen    ]"
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);
                if (selectedMenuItem == "[       Films      ]")
                {
                    Console.Clear();
                    selectedinstance = films.Filmmenu();
                    Cart.UpdateFilmPrice(selectedinstance);

                    string selectedmoviename = "";
                    string selectedmoviestarttime = "";
                    string selectedmovieendtime = "";
                    string selectedmovieprice = "";
                    for (int i = 0; i<movielist.FilmArray.Length;i++ )
                    {
                        if (selectedinstance != null && selectedinstance.StartDateTime != null && selectedinstance.MovieID == movielist.FilmArray[i].ID)
                        {
                            selectedmoviename = movielist.FilmArray[i].Name;
                            selectedmoviestarttime = selectedinstance.StartDateTime.Split(" ")[1];
                            selectedmovieendtime = selectedinstance.EndDateTime.Split(" ")[1];
                            selectedmovieprice = selectedinstance.Price+"";  
                        }
                    }
                    Cart.UpdateFilmName(selectedmoviename);
                    Cart.UpdateFilmStartnEnd(selectedmoviestarttime, selectedmovieendtime);
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
                else if (selectedMenuItem == "[    Reviewmenu    ]")
                {
                    Console.Clear();
                    // open de review list of json
                    Reviews.Revmenu();
                    back();
                   
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
                    ResetIndex();
                    Console.Clear();
                    // Reset User ID
                    Program.UID = -1;
                    mainmenubool = false;
                }
                Console.Clear();
            }
        }
        new public static void back()
        {
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.Backspace)
            {
                Console.Clear();
                Mainmenu();
            }
        }
    } // ./ Main Menu

    public class AdminMenu : MenuController {  // Inherits from Abstract Class
        // Admin Menu
        public static void Mainmenu() {
            // Start the Menu
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();
                List<string> Mainscreen = new List<string>() {
                "[      Reviews Beheren     ]" , // 0
                "[     Gebruikers Beheren   ]" , // 1
                "[        Mijn Account      ]" , // 2
                "[         Uitloggen        ]"   // 3
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);
       
              if (selectedMenuItem == Mainscreen[0]) // Reviews Beheren
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    ShowReviewsAdmin reviewAdmin = new ShowReviewsAdmin();
                    reviewAdmin.review_list();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[1]) // Gebruikers Beheren
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    ShowUserAccounts usersAdmin = new ShowUserAccounts();
                    ShowUserAccounts.mainMenu();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[2]) // Mijn Account
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    // Open Account Settings
                    MyAccount User = new MyAccount(Program.UID);
                    User.OpenMenu();
                    // ./ Contents
                    back();    
                } // Mainscreen.Count -1 to always get the last element of the list, as Logout is always last. 
                else if (selectedMenuItem == Mainscreen[Mainscreen.Count - 1]) // Logout
                {
                    ResetIndex();
                    Console.Clear();
                    // Reset User ID
                    Program.UID = -1;
                    mainmenubool = false;
                }
                Console.Clear();
            }
        }
        new public static void back()
        {
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.Backspace)
            {
                Console.Clear();
                Mainmenu();
            }
        }
    }
    public class Guest : MainMenu  // Inherits from Abstract Class
    {
        // Guest Menu
        new public static void Mainmenu()
        {
            string filmJSONPath = Path.GetFullPath(@"FilmList.json");
            string jsonStringFilmList = File.ReadAllText(filmJSONPath);
            var filmList = new ConsoleApp1.FilmArr();
            filmList = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmList);
            Filmlist films = new Filmlist();
            Filminstance selectedinstance = new Filminstance();

            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();
                List<string> Mainscreen = new List<string>() {
                "[       Films      ]" , // 0
                "[Hapjes en drankjes]" , // 1
                "[  Informatiemenu  ]" , // 2
                "[    Reviewmenu    ]" , // 3
                "[   Winkelmandje   ]" , // 4
                "[     Afsluiten    ]"   // 6
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);

                if (selectedMenuItem == Mainscreen[0]) // Films Toevoegen
                {
                    // Contents
                    ResetIndex();
                    Console.Clear();
                    selectedinstance = films.Filmmenu();


                    string selectedmoviename = "";
                    string selectedmoviestarttime = "";
                    string selectedmovieendtime = "";
                    string selectedmovieprice = "";
                    for (int i = 0; i < movielist.FilmArray.Length; i++)
                    {
                        if (selectedinstance != null && selectedinstance.StartDateTime != null && selectedinstance.MovieID == movielist.FilmArray[i].ID)
                        {
                            selectedmoviename = movielist.FilmArray[i].Name;
                            selectedmoviestarttime = selectedinstance.StartDateTime.Split(" ")[1];
                            selectedmovieendtime = selectedinstance.EndDateTime.Split(" ")[1];
                            selectedmovieprice = selectedinstance.Price + "";
                        }
                    }
                    Cart.UpdateFilmName(selectedmoviename);
                    Cart.UpdateFilmStartnEnd(selectedmoviestarttime, selectedmovieendtime);
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[1]) // Hapjes & Drankjes
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    ConsoleApp1.Products.Productmenu();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[2]) // Info Paneel
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    Infopanels panelObj = new Infopanels();
                    panelObj.InfoScreen();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[3]) // Reviews
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    // open de review list of json
                    Reviews.Revmenu();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[4]) // Winkelmandje
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents    
                    // open de seats json + snackselected json
                    // if email == seats.email && email == snacksselected.email
                    // zo misschien info ophalen per account
                    Cart.PaymentScreen();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[Mainscreen.Count - 1]) // Exit
                {
                    ResetIndex();
                    Console.Clear();
                    mainmenubool = false;
                }
                Console.Clear();
            }
        }
        new public static void back()
        {
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.Backspace)
            {
                Console.Clear();
                Mainmenu();
            }
        }
    }
}