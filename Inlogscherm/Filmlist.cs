using System;
using System.Text.Json;
using System.IO;

namespace ConsoleApp1
{
    public class Movielist
    {
        public string name { get; set; }
        public Movie[] movies { get; set; }
    }

    public class Movie
    {
        public string id { get; set; }
        public string moviename { get; set; }
        public string director { get; set; }
        public string[] cast { get; set; }
        public string date { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public int roomid { get; set; }
        public string screentype { get; set; }
        public string[] genre { get; set; }
    }

    public class filmmenu
    {
        public static Movie Filmmenu(string[] items, int index = 0, bool done = false)
        {
            string moviesjson = File.ReadAllText(Path.GetFullPath(@"movies.json"));
            var movielist = JsonSerializer.Deserialize<Movielist>(moviesjson);
            while (!done)
            {
                for (int i = 0; i < items.Length; i++)
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
                    Console.WriteLine($"Wilt u {items[index]} selecteren? (j/n)");
                    var input = Console.ReadLine();
                    if (input == "j" || input == "J")
                    {
                        Console.Clear();
                        done = true;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }

                Console.Clear();
            }
            return movielist.movies[index]; 
        }
        
    }

}
