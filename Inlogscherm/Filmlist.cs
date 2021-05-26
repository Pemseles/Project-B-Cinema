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
            if (movieList.Count > 0)
            {
                bool done = false;
                while (!done)
                {
                    for (int i = 0; i < movieList.Count; i++)
                    {
                        if (i == index)
                        {
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray;  Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(movieList[i].Moviename + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Tijd: " + movieList[i].StartTime + " tot " + movieList[i].EndTime + "\n");
                            Console.ResetColor();
                            Console.Write("                                                ");
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Zaal: " + movieList[i].RoomId + "Type: " + movieList[i].ScreenType + "\n");
                        }
                        else
                        {
                            Console.Write("");
                            Console.WriteLine($"                                                {movieList[i].Moviename}\n                                                Tijd: {movieList[i].StartTime} tot {movieList[i].EndTime}\n                                                Zaal: {movieList[i].RoomId} Type: {movieList[i].ScreenType}\n");
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
            else
            {
                Console.WriteLine($"                              Geen films gevonden vandaag, probeer alstublieft later opnieuw.\n");
                Console.WriteLine($"                                    Druk op Enter om terug te gaan naar het hoofdmenu.\n");
                ConsoleKeyInfo ckey = Console.ReadKey();
                while (ckey.Key != ConsoleKey.Enter)
                {
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
