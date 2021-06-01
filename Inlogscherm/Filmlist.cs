using System;
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
        public static FilmArray FilmMenu(string[] items, string vandaag = "nee", bool done = false, int index = 0)
        {
            string moviesjson = File.ReadAllText(Path.GetFullPath(@"FilmList.json"));
            var movielist = JsonSerializer.Deserialize<Rootobject>(moviesjson);
            string movieinstancesjson = File.ReadAllText(Path.GetFullPath(@"FilmInstances.json"));
            var movieinstancelist = JsonSerializer.Deserialize<Rootobject2>(movieinstancesjson);
            string filmname = "";
            while (!done)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == index)
                    {
                        Console.Write("");
                        if (vandaag == "ja")
                        {
                            for (int j = 0; j < movielist.FilmArray.Length; j++)
                            {
                                if (movielist.FilmArray[j].Name == items[i])
                                {
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write(items[i] + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("Tijd: " + movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[1] + " tot " + movieinstancelist.FilmInstances[i].EndDateTime.Split(" ")[1] + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("Zaal: " + movieinstancelist.FilmInstances[i].TheaterhallID + " Type: " + movieinstancelist.FilmInstances[i].Type + "\n");

                                }
                            }
                        }
                        else
                        {
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(items[i] + "\n");
                        }
                    }
                    else
                    {
                        if (vandaag == "ja")
                        {
                            for (int j = 0; j < movielist.FilmArray.Length; j++)
                            {
                                if (movielist.FilmArray[j].Name == items[i])
                                {
                                    Console.WriteLine($"                                                {items[i]}\n                                                Tijd: {movieinstancelist.FilmInstances[i].StartDateTime.Split(" ")[1]} tot {movieinstancelist.FilmInstances[i].EndDateTime.Split(" ")[1]}\n                                                Zaal: {movieinstancelist.FilmInstances[i].TheaterhallID} Type: {movieinstancelist.FilmInstances[i].Type}\n");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"                                                {items[i]}");
                        }
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

    }

}
