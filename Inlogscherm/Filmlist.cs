using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Globalization;

namespace ConsoleApp1
{
    public class Filmsuperclass
    {
        public FilmArray[] FilmArray { get; set; }
        public Filminstance[] FilmInstances { get; set; }

    }

    public class FilmArray
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string[] Genres { get; set; }
        public int AgeRating { get; set; }
    }

    public class Filminstance
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int TheaterhallID { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }

    public class Filmlist
    {
        public FilmArr FilmList = new FilmArr();
        public Filminstance Filmmenu()
        {
            Filminstance selectedinstance = new Filminstance();
            bool done = false;
            int index = 0;
            while (!done)
            {
                string filmJSONPath = Path.GetFullPath(@"FilmList.json");
                string jsonStringFilmList = File.ReadAllText(filmJSONPath);
                FilmList = new FilmArr();
                FilmList = JsonSerializer.Deserialize<FilmArr>(jsonStringFilmList);
                Filmlist films = new Filmlist();
                int index2 = 0;
                bool done2 = false;

                Console.Clear();
                List<string> items = new List<string>() {
                "[    Alle films    ]" ,
                "[    Zoek films    ]" ,
                "[   Datum kiezen   ]" ,
                };
                Logo.Print();
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
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index == items.Count - 1)
                    {
                        index = 0;
                    }
                    else { index++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        index = items.Count - 1;
                    }
                    else { index--; }
                }
                else if (ckey.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    if (Program.Level >= 3)
                    {
                        AdminMenu.Mainmenu();
                    }
                    else if (Program.UID == -1)
                    {
                        Guest.Mainmenu();
                    }
                    else
                    {
                        MainMenu.Mainmenu();
                    }
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    if ( items[index]== "[    Alle films    ]")
                    {

                        Console.Clear();
                        done = true;
                        string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
                        var movielist = JsonSerializer.Deserialize<Filmsuperclass>(moviesjson);
                        string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.JSON"));
                        Filmsuperclass movieinstancelist = JsonSerializer.Deserialize<Filmsuperclass>(movieinstancesjson);
                        string[] FilmList = new string[movielist.FilmArray.Length];
                        for (int i = 0; i < movielist.FilmArray.Length; i++)
                        {
                            FilmList[i] = movielist.FilmArray[i].Name;
                        }
                        var selectedmovie = films.Filmlijst(FilmList);
                        
                        List<Filminstance> Filminstancelist = new List<Filminstance>();
                        for (int i = 0;i<movieinstancelist.FilmInstances.Length;i++)
                        {
                            if (selectedmovie.ID==movieinstancelist.FilmInstances[i].MovieID)
                            {
                                Filminstancelist.Add(movieinstancelist.FilmInstances[i]);
                            }
                        }

                        bool done3 = false;
                        int index3 = 0;
                        while (!done3)
                        {
                            for (int i = 0; i < Filminstancelist.Count; i++)
                            {
                                if (i==0)
                                {
                                    Logo.Print();
                                }
                                if (i == index3)
                                {
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write(selectedmovie.Name + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("Tijd: " + Filminstancelist[i].StartDateTime + " tot " + Filminstancelist[i].EndDateTime + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("Zaal: " + Filminstancelist[i].TheaterhallID + " Type: " + Filminstancelist[i].Type + "\n");
                                }
                                else
                                {
                                    Console.Write("                                                ");
                                    Console.Write(selectedmovie.Name + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.Write("Tijd: " + Filminstancelist[i].StartDateTime + " tot " + Filminstancelist[i].EndDateTime + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.WriteLine("Zaal: " + Filminstancelist[i].TheaterhallID + " Type: " + Filminstancelist[i].Type + "\n");
                                }
                                Console.ResetColor();
                            }

                            ConsoleKeyInfo ckey3 = Console.ReadKey();
                            if (ckey3.Key == ConsoleKey.DownArrow)
                            {
                                if (index3 == Filminstancelist.Count - 1)
                                {
                                    index3 = 0;
                                }
                                else { index3++; }
                            }
                            else if (ckey3.Key == ConsoleKey.UpArrow)
                            {
                                if (index3 <= 0)
                                {
                                    index3 = Filminstancelist.Count - 1;
                                }
                                else { index3--; }
                            }
                            else if (ckey3.Key == ConsoleKey.Backspace)
                            {
                                Console.Clear();
                                if (Program.Level >= 3)
                                {
                                    AdminMenu.Mainmenu();
                                }
                                else if (Program.UID == -1)
                                {
                                    Guest.Mainmenu();
                                }
                                else
                                {
                                    MainMenu.Mainmenu();
                                }
                            }
                            else if (ckey3.Key == ConsoleKey.Enter)
                            {
                                
                                Console.Clear();
                                selectedinstance = Filminstancelist[index3];

                                MainMenu.Cart.UpdateFilmName(selectedmovie.Name);
                                MainMenu.Cart.UpdateFilmStartnEnd(selectedinstance.StartDateTime, selectedinstance.EndDateTime);
                                MainMenu.Cart.UpdateFilmPrice(selectedinstance);
                                int zaal = selectedinstance.TheaterhallID;
                                int movieid = selectedinstance.MovieID;
                                Theatherhalls.Moviehall(zaal, movieid);
                                Console.SetWindowSize(120, 30);
                            }
                            Console.Clear();
                        }

                    }
                    else if (items[index] == "[    Zoek films    ]")
                    {
                        Console.Clear();
                        Logo.Print();
                        done = true;
                        Console.Write("Geef hier op wat u zoekt :");

                        string searchClassInput = Console.ReadLine();
                        SearchClass search1 = new SearchClass(searchClassInput);
                        List<Film> searchList = search1.FilmSearch(FilmList);
                        string searchListString = search1.FilmLengthCheck(searchList);
                        Console.Clear();
                        Logo.Print();
                        Console.WriteLine(searchListString);
                        Console.ReadLine();
                    }
                    else if (items[index] == "[   Datum kiezen   ]")
                    {
                        Console.Clear();
                        done = true;
                        var today = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        var tomorrow = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        var dayaftertomorrow = DateTime.Today.AddDays(2).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        while (!done2)
                        {
                            List<string> items2 = new List<string>() {
                            "[      Vandaag     ]" ,
                            "[      Morgen      ]" ,
                            "[     Overmorgen   ]" ,
                            "[  Datum invoeren  ]" ,
                        };
                            Logo.Print();
                            for (int j = 0; j < items2.Count; j++)
                        {
                            if (j == index2)
                            {
                                Console.Write("                                                ");
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;

                                Console.WriteLine(items2[j]);
                            }
                            else
                            {
                                Console.Write("                                                ");
                                Console.WriteLine(items2[j]);
                            }
                            Console.ResetColor();
                        }

                        ConsoleKeyInfo ckey2 = Console.ReadKey();
                            if (ckey2.Key == ConsoleKey.DownArrow)
                            {
                                if (index2 == items2.Count - 1)
                                {
                                    index2 = 0;
                                }
                                else { index2++; }
                            }
                            else if (ckey2.Key == ConsoleKey.UpArrow)
                            {
                                if (index2 <= 0)
                                {
                                    index2 = items2.Count - 1;
                                }
                                else { index2--; }
                            }
                            else if (ckey2.Key == ConsoleKey.Backspace)
                            {
                                Console.Clear();
                                Filmmenu();
                            }
                            else if (ckey2.Key == ConsoleKey.Enter)
                            {
                                if (items2[index2] == "[      Vandaag     ]")
                                {
                                    done2 = true;
                                    Console.Clear();
                                    selectedinstance = films.OpDatum(today);
                                }
                                else if (items2[index2] == "[      Morgen      ]")
                                {
                                    done2 = true;
                                    Console.Clear();
                                    selectedinstance = films.OpDatum(tomorrow);
                                }
                                else if (items2[index2] == "[     Overmorgen   ]")
                                {
                                    done2 = true;
                                    Console.Clear();
                                    selectedinstance = films.OpDatum(dayaftertomorrow);
                                }
                                else if (items2[index2] == "[  Datum invoeren  ]")
                                {
                                    Console.Clear();
                                    Console.WriteLine("                                                Welke dag? (dd/MM/jjjj)");
                                    Console.Write("                                                ");
                                    var other = Console.ReadLine();
                                    Console.Clear();
                                    done2 = true;
                                    selectedinstance = films.OpDatum(other);
                                }
                            }
                            Console.Clear();

                        }
                    }
                }
            }
            return selectedinstance;
        }





        public FilmArray Filmlijst(string[] items)
        {
            string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
            var movielist = JsonSerializer.Deserialize<Filmsuperclass>(moviesjson);
            string filmname = "";
            bool done = false;
            int index = 0;
            while (!done)
            {
                Logo.Print();
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == index)
                    {
                        Console.Write("                                                ");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(items[i] + "\n");
                        
                    }
                    else
                    {
                        Console.WriteLine($"                                                {items[i]}");
                    }


                    Console.ResetColor();
                }

                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index == items.Length - 1)
                    {
                        index = 0;
                    }
                    else { index++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        index = items.Length - 1;
                    }
                    else { index--; }
                }
                else if (ckey.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    Filmmenu();
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Logo.Print();
                    Console.Write("      ");
                    Console.WriteLine($"Wilt u {items[index]} selecteren? Zo ja klik op enter, zo nee klik op backspace.");
                    Console.Write("      ");
                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        filmname = items[index];
                        done = true;
                    }
                    else if (input.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                    }
                }

                Console.Clear();
            }
            for (int i = 0; i < movielist.FilmArray.Length; i++)
            {
                if (movielist.FilmArray[i].Name == filmname)
                {
                    return movielist.FilmArray[i];
                }
            }
            return movielist.FilmArray[index];
        }

        


        public Filminstance OpDatum(string selectedday)
        {
            bool done = false;
            int index = 0;

            string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.JSON"));
            Filmsuperclass movielist = JsonSerializer.Deserialize<Filmsuperclass>(moviesjson);
            string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.JSON"));
            Filmsuperclass movieinstancelist = JsonSerializer.Deserialize<Filmsuperclass>(movieinstancesjson);
            int selectedmovieid = -1;
            Filminstance selectedmovieinstance = null;

            string todaystringlist = "";
            string todaystartdatetime = "";
            string todayenddatetime = "";
            string movietypestring = "";
            string hallidstring = "";
            string instanceid = "";

            for (int i = 0; i < movieinstancelist.FilmInstances.Length; i++)
            {
                if (movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[0]== selectedday)   //05/29/2021 of 05/30/2021
                {
                    for (int j = 0; j < movielist.FilmArray.Length; j++)
                    {
                        if (movieinstancelist.FilmInstances[i].MovieID == movielist.FilmArray[j].ID)
                        {
                            todaystringlist += movielist.FilmArray[j].Name +"&&&&";
                            todaystartdatetime += movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[1] + "&&&&"; //05:50 of 09:20
                            todayenddatetime += movieinstancelist.FilmInstances[i].EndDateTime.Split(" ")[1] + "&&&&";
                            movietypestring += movieinstancelist.FilmInstances[i].Type + "&&&&";
                            hallidstring += movieinstancelist.FilmInstances[i].TheaterhallID + "&&&&";
                            instanceid += movieinstancelist.FilmInstances[i].ID + "&&&&";
                        }
                    }
                }
            }
            string[] todayarray = todaystringlist.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] todaystarttime = todaystartdatetime.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] todayendtime = todayenddatetime.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] type = movietypestring.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] hallid = hallidstring.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] id = instanceid.Split(new string[] { "&&&&" }, StringSplitOptions.RemoveEmptyEntries);

            while (!done)
            {
                if (todayarray.Length != 0)
                {
                    for (int i = 0; i < todayarray.Length; i++)
                    {   if (i==0)
                        {
                            Logo.Print();
                        }
                        if (i == index)
                        {
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(todayarray[i] + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Tijd: " + todaystarttime[i] + " tot " + todayendtime[i] + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Zaal: " + hallid[i] + " Type: " + type[i] + "\n");
                        }
                        else
                        {
                            Console.Write("                                                ");
                            Console.Write(todayarray[i] + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.Write("Tijd: " + todaystarttime[i] + " tot " + todayendtime[i] + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.WriteLine("Zaal: " + hallid[i] + " Type: " + type[i] + "\n");
                        }
                        Console.ResetColor();
                    }

                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.DownArrow)
                    {
                        if (index == todayarray.Length - 1)
                        {
                            index = 0;
                        }
                        else { index++; }
                    }
                    else if (ckey.Key == ConsoleKey.UpArrow)
                    {
                        if (index <= 0)
                        {
                            index = todayarray.Length - 1;
                        }
                        else { index--; }
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        Filmmenu();
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Logo.Print();
                        Console.Write("                        ");
                        Console.WriteLine($"Wilt u {todayarray[index]} selecteren? (enter om door te gaan)");
                        ConsoleKeyInfo ckey2 = Console.ReadKey();
                        if (ckey2.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            done = true;
                            selectedmovieid = int.Parse(id[index]);
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                    for (int i = 0; i < movieinstancelist.FilmInstances.Length; i++)
                    {
                        if (movieinstancelist.FilmInstances[i].ID == selectedmovieid)
                        {
                            MainMenu.Cart.UpdateFilmName(todayarray[index]);
                            MainMenu.Cart.UpdateFilmStartnEnd(movieinstancelist.FilmInstances[i].StartDateTime, movieinstancelist.FilmInstances[i].EndDateTime);
                            selectedmovieinstance = movieinstancelist.FilmInstances[i];                         
                            MainMenu.Cart.UpdateFilmPrice(selectedmovieinstance);
                            int zaal = movieinstancelist.FilmInstances[i].TheaterhallID;
                            int movieid = movieinstancelist.FilmInstances[i].MovieID;
                            Theatherhalls.Moviehall(zaal, movieid);
                            Console.SetWindowSize(120, 30);
                        }
                        
                    }
                    
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("                                                Geen films gevonden.");
                    Console.ReadLine();
                    done = true;
                    Console.Clear();
                }
            }
            return selectedmovieinstance;   
        }
    }

}
