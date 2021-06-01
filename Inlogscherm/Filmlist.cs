using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace ConsoleApp1
{
    public class Rootobject
    {
        public FilmArray[] FilmArray { get; set; }


    }

    public class FilmArray
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string[] Genres { get; set; }
        public int AgeRating { get; set; }
    }


    public class Rootobject2
    {
        public Filminstance[] FilmInstances { get; set; }
    }

    public class Filminstance
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int TheaterhallID { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public int Active { get; set; }
    }



    public class Filmlist
    {

        public static FilmArr filmList;
        public static void Filmmenu(int index = 0, bool done = false)
        {
            while (!done)
            {
                string filmJSONPath = Path.GetFullPath(@"FilmList.json");
                string jsonStringFilmList = File.ReadAllText(filmJSONPath);
                filmList = new FilmArr();
                filmList = JsonSerializer.Deserialize<FilmArr>(jsonStringFilmList);

                Console.Clear();
                List<string> items = new List<string>() {
                "[    Alle films    ]" ,
                "[    Zoek films    ]" ,
                "[  Datum invoeren  ]" ,
                 };
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
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    if ( items[index]== "[    Alle films    ]")
                    {
                        Console.Clear();
                        done = true;
                        string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
                        var movielist = JsonSerializer.Deserialize<Rootobject>(moviesjson);
                        string[] FilmList = new string[movielist.FilmArray.Length];
                        for (int i = 0; i < movielist.FilmArray.Length; i++)
                        {
                            FilmList[i] = movielist.FilmArray[i].Name;
                        }
                        var selectedmovie = Filmlist.Filmlijst(FilmList);
                        Console.WriteLine($"                                                U heeft {selectedmovie.Name} geselecteerd.");
                        Console.ReadLine();
                    }
                    else if (items[index] == "[    Zoek films    ]")
                    {
                        Console.Clear();
                        done = true;
                        Console.Write("Geef hier op wat u zoekt :");
                        string searchClassInput = Console.ReadLine();
                        SearchClass search1 = new SearchClass(searchClassInput);
                        List<Film> searchList = search1.FilmSearch(filmList);
                        string searchListString = search1.FilmLengthCheck(searchList);
                        Console.Clear();
                        Console.WriteLine(searchListString);
                        Console.ReadLine();
                    }
                    else if (items[index] == "[  Datum invoeren  ]")
                    {
                        Console.Clear();
                        done = true;
                        string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
                        var movielist = JsonSerializer.Deserialize<Rootobject>(moviesjson);
                        string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.json"));
                        var movieinstancelist = JsonSerializer.Deserialize<Rootobject2>(movieinstancesjson);
                        //var today = DateTime.Now.ToString("dd/MM/yyyy");
                        Console.WriteLine("                                                Welke dag? (dd/mm/jjjj) doe 05/29/2021");
                        Console.Write("                                                ");
                        var today = Console.ReadLine();
                        var selectedmovie = Filmlist.OpDatum(today);
                        Registers.moviehall();

                    }
                }
            }
        }





        public static FilmArray Filmlijst(string[] items, bool done = false, int index = 0)
        {
            string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
            var movielist = JsonSerializer.Deserialize<Rootobject>(moviesjson);
            string filmname = "";
            while (!done)
            {
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
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.Write("                                                ");
                    Console.WriteLine($"Wilt u {items[index]} selecteren? (j/n)");
                    Console.Write("                                                ");
                    var input = Console.ReadLine();
                    if (input == "j" || input == "J")
                    {
                        Console.Clear();
                        filmname = items[index];
                        done = true;
                    }
                    else
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

        


        public static FilmArray OpDatum(string today, bool done = false, int index = 0)
        {
            Console.Clear();
            string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
            var movielist = JsonSerializer.Deserialize<Rootobject>(moviesjson);
            string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.json"));
            var movieinstancelist = JsonSerializer.Deserialize<Rootobject2>(movieinstancesjson);
            string filmname = "";

            string todaystringlist = "";
            for (int i = 0; i < movieinstancelist.FilmInstances.Length; i++)
            {
                if (movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[0]==today)
                {
                    for (int j = 0; j < movielist.FilmArray.Length; j++)
                    {
                        if (movieinstancelist.FilmInstances[i].MovieID == movielist.FilmArray[j].ID)
                        {
                            todaystringlist += movielist.FilmArray[j].Name +"&&&&";
                        }
                    }
                }
            }


            string[] todaylist = todaystringlist.Split("&&&&");


            while (!done)
            {
                Console.Clear();
                for (int i = 0; i < todaylist.Length; i++)
                {

                    if (i == index)
                    {
                        Console.Write("                                                ");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(todaylist[i] + "\n");
                        Console.ResetColor();
                        //Console.Write("                                                ");
                        //Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        //Console.Write("Tijd: " + movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[1] + " tot " + movieinstancelist.FilmInstances[i].EndDateTime.Split(" ")[1] + "\n");
                        //Console.ResetColor();
                        //Console.Write("                                                ");
                        //Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        //Console.WriteLine("Zaal: " + movieinstancelist.FilmInstances[i].TheaterhallID + " Type: " + movieinstancelist.FilmInstances[i].Type + "\n");
                    }
                    else
                     {
                        Console.Write("                                                ");
                        Console.Write(todaylist[i] + "\n");
                        Console.ResetColor();
                        //Console.Write("                                                ");
                        //Console.Write("Tijd: " + movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[1] + " tot " + movieinstancelist.FilmInstances[i].EndDateTime.Split(" ")[1] + "\n");
                        //Console.ResetColor();
                        //Console.Write("                                                ");
                        //Console.WriteLine("Zaal: " + movieinstancelist.FilmInstances[i].TheaterhallID + " Type: " + movieinstancelist.FilmInstances[i].Type + "\n");
                    }
                    Console.ResetColor();
                }

                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index == todaylist.Length - 1)
                    {
                        index = 0;
                    }
                    else { index++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        index = todaylist.Length - 1;
                    }
                    else { index--; }
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.Write("                                                ");
                    Console.WriteLine($"Wilt u {todaylist[index]} selecteren? (j/n)");
                    Console.Write("                                                ");
                    var input = Console.ReadLine();
                    if (input == "j" || input == "J")
                    {
                        Console.Clear();
                        filmname = todaylist[index];
                        done = true;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
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
    }

}
