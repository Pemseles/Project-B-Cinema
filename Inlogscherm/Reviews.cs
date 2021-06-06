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
    public class Review
    {
        public int ID { get; set;  }
        public int UID { get; set; }
        public int TagID { get; set; }
        public int RevID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public string UploadDate { get; set; }
        public bool Active { get; set; }
    }

    /*
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
    */
    class Reviews : MenuController
    {
        string RevPath = Path.GetFullPath(@"Review.JSON");
        static int index3 = 0;
        static int index2 = 0;
        static int index = 0;
        static string star = "";
        public static List<string[]> reviews = new List<string[]>();
        private ConsoleApp1.Accounts activeAccount = new Accounts();
        public List<string> AccountMenu;

        //public int index;
    
        public static string stars()
        {
            while (true)
            {
                star = "";
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
        public static string privacy()
        {
            int J = 0;
            string name = "";
            while (true)
            {
                Console.WriteLine("wilt u uw review anoniem plaatsen?");
                if (J == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("[ja ]");
                    Console.ResetColor();
                    Console.WriteLine("[nee]");
                }
                if (J == 1)
                {
                    Console.ResetColor();
                    Console.WriteLine("[ja ]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("[nee]");
                    Console.ResetColor();
                }
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.UpArrow)
                {
                    J = 0;
                }
                else if (ckey.Key == ConsoleKey.DownArrow)
                {
                    J = 1;
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    if (J == 0)
                    {
                        name = "Anoniem";
                    }
                    else
                    {
                        Tuple<string, string> Fullname = Accounts.GetFullname(Program.UID);
                        name = $"{Fullname.Item1} {Fullname.Item2}";
                    }
                    break;
                }
                Console.Clear();
            }
            return name;
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
                if (movies[i].Length > len)
                {
                    len = movies[i].Length;
                }
            }
            for (int i = 0; i < movies.Count; i++)
            {
                if (len > movies[i].Length)
                {
                    if ((len - movies[i].Length) % 2 == 0)
                    {
                        string front = "";
                        string back = "";
                        int leng = len - movies[i].Length;
                        for (int j = 0; j < leng / 2; j++)
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
        public static void make_review(int kind)
        {

            string[] rev_arr = new string[4];
            int revs = 0;
            int place = 0;
            int total_revs = reviews.Count;
            rev_arr[0] = privacy();
            if (kind == 0)
            {
                rev_arr[1] = review_menu();
            }
            else if(kind == 1)
            {
                Console.Write("waar gaat uw review over");
                rev_arr[1] = Topic(kind);
            }
            else if (kind == 2)
            {
                Console.Write("waar gaat uw review over");
                rev_arr[1] = Console.ReadLine();
            }
            rev_arr[2] = review_text(Console.ReadLine());
            rev_arr[3] = stars();
            reviews.Add(rev_arr);

            ReviewCreation.AddReview(Program.UID, kind,index3 ,rev_arr[0], rev_arr[1],rev_arr[2],rev_arr[3].Length, $"{DateTime.Today}");
        }
        public static void review_list()
        {
            var rev_list = ReviewCreation.GetAllReviews();
            rev_list.Reverse();
            int place = 0;
            int revs = 0;

            //ReviewCreation.FilterReviews();
            int total_revs = rev_list.Count;
            if(total_revs > 0) {
                while (true)
                {
                    Console.Write("                                  Gebruikersnaam: " + rev_list[revs].Author + "                "); Console.WriteLine("waardering: " + rev_list[revs].Stars);
                    Console.WriteLine();
                    Console.WriteLine("                                       [ " + rev_list[revs].Title + " ]");
                    Console.WriteLine();
                    Console.Write("                                  "); Console.Write(rev_list[revs].Description);
                    Console.WriteLine();

                    if (place == 0)
                    {
                        Console.WriteLine();
                        Console.Write("                                                 ");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("< Vorige >");
                        Console.ResetColor();
                        Console.Write(" ");
                        Console.Write($"< {revs + 1} / {total_revs} >");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 1)
                    {
                        Console.WriteLine();
                        Console.Write("                                                 ");
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($"< {revs + 1} / {total_revs} >");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                        Console.Write("< Volgende >");
                        Console.ResetColor();

                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.LeftArrow || ckey.Key != ConsoleKey.RightArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.RightArrow)
                    {
                        place = 1;
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {
                        place = 0;
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        if (place == 0)
                        {
                            if (revs > 0)
                                revs -= 1;
                        }
                        if (place == 1)
                        {
                            if (revs < total_revs-1)
                                revs += 1;
                        }
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
            }
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
                if (index == 4)
                {
                    index = 4;
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
        public static int max_len()
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
                if (movies[i].Length > len)
                {
                    len = movies[i].Length;
                }
            }
            return len;
        }
        public static string Topic(int kind)
        {
            string topic = "";
            int len = max_len();
            topic = Console.ReadLine();
            if(topic.Length > len)
            {
                while (true)
                {
                    Console.WriteLine("Je onderwerp is te lang");
                    topic = Console.ReadLine();
                    if (topic.Length < len)
                    {
                        break;
                    }
                }
            }
            else if ((len - topic.Length) % 2 == 0)
            {
                string front = "";
                string back = "";
                int leng = len - topic.Length;
                for (int j = 0; j < leng / 2; j++)
                {
                    front += " ";
                    back += " ";
                }
                topic = front + topic + back;

            }
            else 
            {
                string front = "";
                string back = " ";
                int leng = len - topic.Length;
                for (int j = 0; j < leng / 2; j++)
                {
                    front += " ";
                    back += " ";
                }
                topic = front + topic + back;
            }
            return topic;
        }

        public static void Revmenu()
        {

            bool reviewscreenbool = true;
            while (reviewscreenbool == true)
            {
                List<string> reviewscreen = new List<string>() {
                "[    Recensie maken    ]",
                "[  Recensies bekijken  ]",
                "[      Terugkeren      ]",
                 };
                string selectedMenuItem = rev_menu(reviewscreen); // Read write a Review
                if (selectedMenuItem == reviewscreen[0])
                {
                    Console.Clear();
                    make_review(0);
                    back();
                }
                else if (selectedMenuItem == reviewscreen[1]) // Read Reviews
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == reviewscreen[reviewscreen.Count - 1]) // Back
                {
                    Console.Clear();
                    reviewscreenbool = false;
                    ResetIndex();
                    Console.Clear();
                    // Contents

                    // ./ Contents
                    back();
                }
            }
        }
    }

    public class AddReviewMenu : MenuController  
    {  // Inherits from Abstract Class
        public static void ReadReviewMenu()
        {
            // Start the Menu
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();
                List<string> Mainscreen = new List<string>() {
            "[       Film recensies      ]" , // 0 
            "[      bioscoop recensies   ]" , // 1 
            "[       Zaal recensies      ]" , // 2
            "[         Terugkeren        ]"   // 3
                };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);

                if (selectedMenuItem == Mainscreen[0]) // Read Film Reviews
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[1]) // Read Reviews about CineScope
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[2]) // Read Reviews about Theatherhalls
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

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

        public static void WriteReviewMenu()
        {
            // Start the Menu
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();
                List<string> Mainscreen = new List<string>() {
            "[       Film recensie      ]" , // 0 
            "[     bioscoop recensie    ]" , // 1
            "[       Zaal recensie      ]" , // 2
            "[         Terugkeren       ]"   // 3
                };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);

                if (selectedMenuItem == Mainscreen[0]) // Film Review
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[1]) // CineScope Review
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[2]) // Theather Review
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents

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
    }
}

