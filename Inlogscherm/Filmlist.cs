using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Movielist
    {
        public Movie[] Movies { get; set; }
    }

    public class Movie
    {
        public string Id { get; set; }
        public string Moviename { get; set; }
        public string Director { get; set; }
        public string[] Cast { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int RoomId { get; set; }
        public string ScreenType { get; set; }
        public string[] Genre { get; set; }
    }

    public class FilmMenu
    {
        public FilmMenu() { }
        public Movie Filmmenu(List<Movie> movieList, int index = 0)
        {

            bool done = false;
            while (!done)
            {
                for (int i = 0; i < movieList.Count; i++)
                {
                    if (i == index)
                    {
                        Console.Write("");
<<<<<<< HEAD
<<<<<<< HEAD
=======
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
>>>>>>> parent of 57aff22 (Voortgang Checkout)
                        if (vandaag == "ja")
                        {
                            for (int j = 0; j < movielist.movies.Length; j++)
                            {
                                if (movielist.movies[j].moviename == items[i])
                                {
<<<<<<< HEAD
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray;  Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write(items[i] + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("Tijd: " + movielist.movies[j].starttime + " tot " + movielist.movies[j].endtime + "\n");
                                    Console.ResetColor();
                                    Console.Write("                                                ");
                                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("Zaal: " + movielist.movies[j].roomid + "Type: " + movielist.movies[j].screentype + "\n");
                                    
=======
                                    Console.WriteLine($"{items[i]}\nTijd: {movielist.movies[j].starttime} tot {movielist.movies[j].endtime}\nZaal: {movielist.movies[j].roomid} Type: {movielist.movies[j].screentype}");
>>>>>>> parent of 57aff22 (Voortgang Checkout)
                                }
                            }
                        }
                        else
=======
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        for (int j = 0; j < movieList.Count; j++)
>>>>>>> parent of 37e8c56 (Merge branch 'local' into Thijmen)
                        {
                            Console.WriteLine($"Tijd: {movieList[i].StartTime} tot {movieList[i].EndTime}\nZaal: {movieList[i].RoomId} Type: {movieList[i].ScreenType}");
                        }
                    }
                    else
                    {
                        Console.Write("");
                        for (int j = 0; j < movieList.Count; j++)
                        {
<<<<<<< HEAD
                            for (int j = 0; j < movielist.movies.Length; j++)
                            {
                                if (movielist.movies[j].moviename == items[i])
                                {
<<<<<<< HEAD
                                    Console.WriteLine($"                                                {items[i]}\n                                                Tijd: {movielist.movies[j].starttime} tot {movielist.movies[j].endtime}\n                                                Zaal: {movielist.movies[j].roomid} Type: {movielist.movies[j].screentype}\n");
=======
                                    Console.WriteLine($"{items[i]}\nTijd: {movielist.movies[j].starttime} tot {movielist.movies[j].endtime}\nZaal: {movielist.movies[j].roomid} Type: {movielist.movies[j].screentype}");
>>>>>>> parent of 57aff22 (Voortgang Checkout)
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(items[i]);
                        }    
=======
                            Console.WriteLine($"Tijd: {movieList[i].StartTime} tot {movieList[i].EndTime}\nZaal: {movieList[i].RoomId} Type: {movieList[i].ScreenType}");
                        } 
>>>>>>> parent of 37e8c56 (Merge branch 'local' into Thijmen)
                    }
                    Console.ResetColor();
                }

                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index < movieList.Count - 1)
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
                    Console.WriteLine($"Wilt u {movieList[index].Moviename} selecteren? (j/n)");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "j")
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
            MainMenu.Cart.UpdateFilmIndex(index);
            return movieList[index]; 
        }
    }
}
