using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using static ConsoleApp1.MainMenu;


namespace ConsoleApp1
{
    public class Movie_rev
    {
        public string Movie { get; set; }
        public string Username { get; set; }
        public string Review { get; set; }
        public int Stars { get; set; }
    }
    public class Zaal_rev
    {
        public string Movie { get; set; }
        public string Username { get; set; }
        public string Review { get; set; }
        public int Stars { get; set; }
    }
    public class Cinescope_rev
    {
        public string Movie { get; set; }
        public string Username { get; set; }
        public string Review { get; set; }
        public int Stars { get; set; }
    }
    public class rev_arr
    {
        public Movie_rev[] movie_rev { get; set; }
        public Zaal_rev[] zaal_rev { get; set; }
        public Cinescope_rev[] cinescope_rev { get; set; }
    }
    
    class Review : MenuController
    {
        string RevPath = Path.GetFullPath(@"Review.JSON");
        static int index3 = 0;
        static int index2 = 0;
        static int index = 0;
        static string star = "";
        public static string stars()
        {
            while (true)
            {
                int j = 0;
                Console.Write("hoeveel sterren wilt u geven:");
                for (int i = 0; i < 5; i++)
                {

                    if (i == index2)
                    {

                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                    Console.ResetColor();
                }
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key != ConsoleKey.RightArrow || ckey.Key != ConsoleKey.LeftArrow || ckey.Key != ConsoleKey.Enter)
                {

                }
                if (ckey.Key == ConsoleKey.RightArrow)
                {
                    if (index2 == 4)
                    {
                        index2 = 4;
                    }
                    else
                    {
                        index2++;
                    }
                }
                else if (ckey.Key == ConsoleKey.LeftArrow)
                {
                    if (index2 == 0)
                    {
                        index2 = 0;
                    }
                    else
                    {
                        index2--;
                    }
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    for (int i = 0; i <= index2; i++)
                    {
                        star += "*";
                    }
                    Console.Clear();
                    return star;
                }
                Console.Clear();
                stars();
                return star;


            }

        }
        public static string review_text(string rev)
        {

            string s = "";
            for (int i = 0; i < rev.Length; i++)
            {
                s += rev[i];
                if (rev[i] == '.' || rev[i] == '?' || rev[i] == ',')
                {
                    s += "\n                                  ";
                    
                    if (i + 1 < rev.Length)
                    {
                        if (rev[i + 1] == ' ')
                        {
                            i++;
                        }
                    }
                }
            }
            Console.Clear();
            return s;
        }
        public static string review_menu()
        {
            
            List<string> movies = new List<string>();
            string filmJSONPath = Path.GetFullPath(@"FilmList.json");
            string jsonStringFilmList = File.ReadAllText(filmJSONPath);
            ConsoleApp1.FilmArr filmList = new ConsoleApp1.FilmArr();
            filmList = JsonSerializer.Deserialize<ConsoleApp1.FilmArr>(jsonStringFilmList);
            for (int i = 0; i < filmList.FilmArray.Length; i++)
            {
                movies.Add(filmList.FilmArray[i].Name);
            }
            int len = 0;
            for (int i = 0; i < movies.Count; i++)
            {
                if(movies[i].Length > len)
                {
                    len = movies[i].Length;
                }
            }
            for (int i = 0; i < movies.Count; i++)
            {
                if(len > movies[i].Length)
                {
                    if((len - movies[i].Length) % 2 == 0)
                    {
                        string front = "";
                        string back =  "";
                        int leng = len - movies[i].Length;
                        for (int j = 0; j < leng/2; j++)
                        {
                            front += " ";
                            back += " ";
                        }
                        movies[i] = front + movies[i] + back;
                        
                    }
                    else
                    {
                        string front = "";
                        string back = " ";
                        int leng = len - movies[i].Length;
                        for (int j = 0; j < leng / 2; j++)
                        {
                            front += " ";
                            back += " ";
                        }
                        movies[i] = front + movies[i] + back;
                    }

                }
            }
           
            while (true)
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (i == index3)
                    {
                        Console.Write("                                                ");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"[{movies[i]}]");
                    }
                    else
                    {
                        Console.Write("                                                ");
                        Console.WriteLine($"[{movies[i]}]");
                    }
                    Console.ResetColor();
                }
                
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter)
                {

                }
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index3 == movies.Count - 1)
                    {
                        index3 = movies.Count - 1;
                    }
                    else { index3++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index3 <= 0)
                    {
                        index3 = 0;
                    }
                    else { index3--; }
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    string movie = movies[index3];
                    Console.Clear();
                    return movie;
                }
                Console.Clear();
            }
            review_menu();
            return "";
        }
        public static void review()
        {
            string movie = ""; string rev = ""; string star = "";
            movie = review_menu();
            rev = review_text(Console.ReadLine());
            star = stars();
            Console.Write("                                  Gebruikersnaam: Cinescope" + "                "); Console.WriteLine("waardering: " + star);
            Console.WriteLine();
            Console.WriteLine("                                       [ " + movie + " ]"); 
            Console.WriteLine();
            Console.Write("                                  ");  Console.Write(rev);
            Console.WriteLine();
            Console.Write("                                                 <Vorige>   <1/14>   <Volgende>");
            Console.ReadKey();
            Console.Clear();
        }
        public static string rev_menu(List<string> items)
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
                Console.Clear();
            }
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == 3)
                {
                    index = 3;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 0;
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

        public static void Revmenu()
        {
            
            bool reviewscreenbool = true;
            while (reviewscreenbool == true)
            {
                List<string> reviewscreen = new List<string>() {
                "[   Film reviews   ]" ,
                "[ Bioscoop reviews ]" ,
                "[   Zaal reviews   ]" ,
                "[     terugkeren   ]"
                 };
                string selectedMenuItem = rev_menu(reviewscreen);
                if (selectedMenuItem == "[   Film reviews   ]")
                {
                    Console.Clear();
                    review();
                    back();
                }
                else if (selectedMenuItem == "[ Bioscoop reviews ]")
                {
                    Console.Clear();
                    review();
                    back();
                }
                else if (selectedMenuItem == "[   Zaal reviews   ]")
                {
                    Console.Clear();
                    review();
                    back();
                }
                else if (selectedMenuItem == "[     terugkeren   ]")
                {
                    Console.Clear();
                    reviewscreenbool = false;
                    MainMenu.Mainmenu();
                }
            }
        }
    }
}
/* dit is een test review om te laten zien hoe de indent is, ook is deze tekst bedoeld om te checken of die bij elke . en bij elke , een enter doet. dit is het einde van de test zin.*/