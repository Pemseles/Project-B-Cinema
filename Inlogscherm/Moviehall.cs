using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    class Registers
    {
        private static int people = 100;
        private static int ticket = 5;
        private static List<int> keys = new List<int>();
        private static List<int> res = new List<int>();
        private static int index = 0;
        private static List<int> seats = new List<int>();
        public static void moviehall()
        {
            CursorVisible = false;
            res.Add(1);
            res.Add(23);
            res.Add(33);
            res.Add(15);
            res.Add(4);
            res.Add(18);
            res.Add(19);
            
            //int ticket = Int16.Parse(Console.ReadLine());
            bool a = true;
            //Console.Write("how big is the cinema hall: ");
            //string ans = Console.ReadLine();
            //int people = Int16.Parse(ans);
            if (people > 0 && people <= 150)
            {
                Console.SetWindowSize(70, 20);
            }
            else if (people > 150 && people <= 250)
            {
                Console.SetWindowSize(70, 30);
            }
            else if (people > 250 && people <= 602)
            {
                Console.SetWindowSize(70, 40);
            }



            List<string> movie = new List<string>(people);

            for (int i = 0, j = 1; i < people; i++, j++)
            {
                if (j % 10 == 0 && i != 0)
                {
                    movie.Add($" [ {j} ] \n");
                    j = 0;
                }
                else
                {
                    movie.Add($" [ {j} ] ");
                }
            }

            List<int> x = new List<int>();
            while (ticket > 0)
            {
                // forloop maken
                Console.Clear();
                x.Add(MainScreen(movie, people, keys, res, seats, ticket));
                ticket--;
            }
            Console.Clear();
            string s = "";
            for (int c = 0; c < x.Count; c++)
            {
                s += x[c] + 1 + ",";

            }
            if (x.Count > 1)
            {
                Console.WriteLine($"Je hebt deze stoelen geselecteerd : {s} \n KLopt dit?");
                Console.Write("Maak uw keuze : ");
                Console.WriteLine("Ja");
                Console.WriteLine("Nee");
                switch (Console.ReadLine())
                {
                    case "Ja":
                        // return naar mainpage
                        break;
                    case "Nee":
                        // reset de seats
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Je hebt deze stoel geselecteerd : {s} \n Klopt dit?");
                Console.Write("Maak uw keuze : ");
                Console.WriteLine("Ja");
                Console.WriteLine("Nee");
                switch (Console.ReadLine())
                {
                    case "Ja":
                        // return naar mainpage
                        break;
                    case "Nee":
                        // reset de seats
                        break;
                }
            }
            // homescreen()
            static int MainScreen(List<string> items, int people, List<int> keys, List<int> res, List<int> seats, int ticket)
            {
                for (int i = 0; i < items.Count; i++)
                {

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
                    else if (i >= 30 && i < 40)
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
                    if (index >= people)
                    {
                        index = 0;
                    }
                    else { index += 10; }
                }
                else if (ckey.Key == ConsoleKey.LeftArrow)
                {
                    if (index <= 0)
                    {
                        index = people;
                    }
                    else { index--; }
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
                Console.Clear();                
                MainScreen(items, people, keys, res, seats, ticket);
                return index;
            }


        }
    }
}
