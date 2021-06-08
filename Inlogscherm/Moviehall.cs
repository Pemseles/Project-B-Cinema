using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;

/// <summary>
/// wanneer een zaal word aangemaakt stoelen opslaan in Json (filmzaal json met daar in een aparte array voor elke zaal)
/// wanneer een stoel word gereserveerd moet deze als rood worden weergeven
/// meerdere stoelen moeten tegelijk of achter elkaar gereserveerd kunnen worden
/// ? moeten kaarten gecanceld kunnen worden ? 
/// </summary>


// what to do : 
// alles in een json op laten slaan om te onthouden welke seats beschikbaar zijn of niet
// teruggeven welke stoelen zijn gekozen en een dit klopt knop voor geredirect worden naar main of afreken, moeten ff uitzoeken welk
// 2d 3d 4d

/*using System;
using System.Text.Json;

public class MyDate
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
}
public class Lad
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public MyDate DateOfBirth { get; set; }
}
class Program
{
    static void Main()
    {
        var lad = new Lad
        {
            FirstName = "Markoff",
            LastName = "Chaney",
            DateOfBirth = new MyDate
            {
                year = 1901,
                month = 4,
                day = 30
            }
        };
        var json = JsonSerializer.Serialize(lad);
        File.AppendAllText(AccountPath, JsonConvert.SerializeObject(NewUser,  Formatting.Indented));
    }
}
 */
/*
 {
	"SeatArr" :[
        {
            "Name" : "Christ",
            "Seats" : [0],\
            "Date"  : "12/06/2021",
            "Movie" : "John Wick 4"
            "Zaal" : 6
        }, 
	]
}
string path = Path.GetFullPath(@"Seats.Json")
*/



namespace ConsoleApp1
{
    public class NewRes
    {
        public string Email { get; set; }
        public int[] Seats { get; set; }
        public string Date { get; set; }
        public string Movie { get; set; }
        public string Zaal { get; set; }

    }

    class Theatherhalls
    {
        private static List<int> keys = new List<int>();
        private static List<int> res = new List<int>();
        private static int index = 0;
        private static List<int> seats = new List<int>();
        private static ConsoleApp1.Orders newOrder = new Orders();
        public static void Moviehall(int zaal, int movieid)
        {
            int people = 0;
            if(zaal == 1)
            {
                people = 100;
                movieid = 0;
            }   
            else if(zaal == 2)
            {
                people = 250;
                movieid = 1;
            }
            else
            {
                people = 602;
                movieid = 2;
            }
            // functie in orders int array in for-lopen , functie Add toevoegen6
            CursorVisible = false;
            Console.Write("hoeveel stoelen wilt u reserveren: ");         
            int ticket = Int16.Parse(Console.ReadLine());
            foreach (int seat in newOrder.GetSeatCoords(5))
            {
                res.Add(seat);
            }

            //int ticket = Int16.Parse(Console.ReadLine());
            //bool a = true;
            //Console.Write("how big is the cinema hall: ");
            //string ans = Console.ReadLine();
            //int people = Int16.Parse(ans);
            if (people > 0 && people <= 150)
            {
                Console.SetWindowSize(100, 40);
            }
            else if (people > 150 && people <= 250)
            {
                Console.SetWindowSize(110, 60);
            }
            else if (people > 250 && people <= 602)
            {
                Console.SetWindowSize(240, 70);
            }
            char[] Alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
            ;
            List<string> movie = new List<string>(people);
            if (people == 100 || people == 250)
            {
                for (int i = 0, j = 1, y = 0; i < people; i++, j++)
                {
                    if (j % 10 == 0 && i != 0)
                    {
                        movie.Add($" [ {Alphabet[y]}{j} ] \n");
                        j = 0;
                        y++;
                    }
                    else
                    {
                        movie.Add($" [ {Alphabet[y]}{j} ] ");
                    }
                }
            }
            else
            {
                for (int i = 0, j = 1, y = 0; i < people; i++, j++)
                {
                   if(i == 600 || i == 601)
                    {
                        movie.Add($" [ {Alphabet[y]}{j}  ] ");
                    }
                   else if (j % 24 == 0 && i != 0)
                    {
                        movie.Add($" [ {Alphabet[y]}{j} ] \n");
                        j = 0;
                        y++;
                    }
                    else
                    {
                        movie.Add($" [ {Alphabet[y]}{j} ] ");
                    }
                }
            }

            List<int> x = new List<int>();
            while (ticket > 0)
            {
                // forloop maken
                Console.Clear();
                x.Add(MainScreen(movie, people, keys, res, seats, ticket, movieid));
                ticket--;
            }
            string s = "";
            for (int c = 0; c < x.Count; c++)
            {

                s += $"{Alphabet[x[c] / 10]}{x[c]%10+1}, ";
            }
            if (x.Count > 1)
            {
                
                Console.WriteLine($"Je hebt deze stoel geselecteerd : {s} \nKlopt dit?");
                Console.Write("Zo ja klik op enter, zo nee klik op backspace");
                var check = Console.ReadKey();
                if (check.Key == ConsoleKey.Enter)
                {
                    MainMenu.Cart.UpdateSeats(x);
                    Console.SetWindowSize(120, 30);
                    Console.Clear();
                    if (Program.Level >= 3)
                    {
                        Console.SetWindowSize(120, 30);
                        AdminMenu.Mainmenu();
                    }
                    else if (Program.UID == -1)
                    {
                        Console.SetWindowSize(120, 30);
                        Guest.Mainmenu();
                    }
                    else
                    {
                        Console.SetWindowSize(120, 30);
                        MainMenu.Mainmenu();
                    }

                }
                else if (check.Key == ConsoleKey.Backspace)
                {
                    Console.SetWindowSize(120, 30);
                    // reset de seats   
                }
            }
            // homescreen()
            static int MainScreen(List<string> items, int people, List<int> keys, List<int> res, List<int> seats, int ticket, int movieid)
            {
                
                if(people == 602)
                {
                    Logo.Print_602();
                }
                else
                {
                    Logo.Print_100();
                }
                
                if (people == 100 || people == 250)
                {
                    for (int i = 0, j = 0; i < items.Count; i++)
                    {
                        if (i % 10 == 0)
                        {
                            Console.Write("            ");
                            j++;
                        }
                        if (res.Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(items[i]);
                        }

                        else if (keys.Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else if (i == index)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else if (newOrder.GetVipSeatCoords(movieid).Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else
                        {

                            Console.Write(items[i]);
                        }
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("              _____________________________________________________________________________ ");
                    Console.WriteLine("             |___________________________________Filmdoek__________________________________|");             
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Legenda");
                    Console.WriteLine();
                    Console.Write("Gereserveerd: ");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine();
                    Console.Write("Vip stoelen : ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine();
                    Console.Write("Geselecteerd: ");
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine(); Console.WriteLine();
                    ConsoleKeyInfo ckey = Console.ReadKey();

                    if (ckey.Key == ConsoleKey.RightArrow)
                    {
                        if (index == people)
                        {
                            index = 0;
                        }
                        else { index++; }
                    }
                    else if (ckey.Key == ConsoleKey.UpArrow)
                    {
                        if (index < 0)
                        {
                            index = people - 10;
                        }
                        else { index -= 10; }
                    }
                    else if (ckey.Key == ConsoleKey.DownArrow)
                    {

                        index += 10;
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {

                        index--;
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        if (!keys.Contains(index) && !res.Contains(index))
                        {
                            keys.Add(index);
                            return index;
                        }
                        else
                        {

                            Console.WriteLine("Deze stoel is al gereserveerd, klik op enter om verder te gaan");
                            Console.ReadKey(true);


                        }
                    }
                }
                else
                {
                    for (int i = 0, j = 0; i < items.Count; i++)
                    {
                        if (i == 600)
                        {
                            Console.Write("                                                                                                   ");
                        }
                        if (i % 24 == 0)
                        {
                            Console.Write("            ");
                            j++;
                        }
                        if (res.Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(items[i]);
                        }

                        else if (keys.Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else if (i == index)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else if (newOrder.GetVipSeatCoords(movieid).Contains(i))
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(items[i]);
                        }
                        else
                        {

                            Console.Write(items[i]);
                        }
                        Console.ResetColor();
                    }
                    
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("              ___________________________________________________________________________________________________________________________________________________________________________________________________________ ");
                    Console.WriteLine("             |______________________________________________________________________________________________________Filmdoek_____________________________________________________________________________________________|");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Legenda");
                    Console.WriteLine();
                    Console.Write("Gereserveerd: ");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine();
                    Console.Write("Vip stoelen : ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine();
                    Console.Write("Geselecteerd: ");
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); Console.ResetColor(); Console.WriteLine(); Console.WriteLine();
                    ConsoleKeyInfo ckey = Console.ReadKey();

                    if (ckey.Key == ConsoleKey.RightArrow)
                    {
                        if (index == people)
                        {
                            index = 0;
                        }
                        else { index++; }
                    }
                    else if (ckey.Key == ConsoleKey.UpArrow)
                    {
                        if (index < 0)
                        {
                            index = people - 24;
                        }
                        else { index -= 24; }
                    }
                    else if (ckey.Key == ConsoleKey.DownArrow)
                    {

                        index += 24;
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {

                        index--;
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        if (!keys.Contains(index) && !res.Contains(index))
                        {
                            keys.Add(index);
                            return index;
                        }
                        else
                        {

                            Console.WriteLine("Deze stoel is al gereserveerd, klik op enter om verder te gaan");
                            Console.ReadKey(true);


                        }
                    }
                }
                Console.Clear();                
                MainScreen(items, people, keys, res, seats, ticket, movieid);
                return index;
            }
        }
    }
}
