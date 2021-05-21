using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static System.Console;
using System.IO;


namespace ConsoleApp1
{
    public class Products
    {
        public string productsPath = Path.GetFullPath(@"ProductList.json");
        public string orderPath = Path.GetFullPath(@"Orders.json");

        public static ConsoleApp1.Orders newOrder = new Orders();
        public static ConsoleApp1.Order addOrder = new Order();
        /// <summary>
        // t is geen fucking list, ff aanpassen
        /// </summary>
        private static int index = 0;

        private static string MainScreen(List<string> items)

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

            //bepaalt omhoog of omlaag met de pijltjes
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
            {

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

        public static void resetIndex()
        {
            index = 0;
        }



        // to do:    zorgen dat het loopt door de opties en je t kan selecteren
        //           wanneer er iets geselcteerd wordt zorgen dat het in een Orders JSON opeslagen wordt
        public static void Productmenu()
        { // Euroteken: 20AC

            bool productmenubool = true;
            while (productmenubool == true)
            {
                List<string> ProductMenu = new List<string>()
                    {
                        "[      Snacks      ]" ,
                        "[     Drankjes     ]" ,
                        "[      Alcohol     ]" ,
                        "[   Combi Deals    ]" ,
                        "[       Terug      ]"
                    };
                // kijkt bij welke index de user zich bevind

                string selectedMenuItem = MainScreen(ProductMenu);

                while (selectedMenuItem == "[      Snacks      ]")
                {
                    Console.Clear();
                    var Snacks = newOrder.GetProducts("Snack");
                    int i = 0;
                    foreach (var Snack in Snacks)
                    {
                        {
                            if (i == index)
                            {

                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"[ snack : {Snack.Name},  prijs : {Snack.Price}] ");
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine($"[ snack : {Snack.Name},  prijs : {Snack.Price}] ");
                            }
                            i++;
                            Console.ResetColor();
                        }
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.DownArrow)
                    {
                        if (index == Snacks.Count-1)
                        {
                            index = Snacks.Count-1;
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
                       /* addOrder.Add(Snacks[index].ID); */
                        resetIndex();
                        Console.Clear();
                        break;
                        // add the ID to an order JSON
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        resetIndex();
                        Console.Clear();
                        break;
                    }

                    // haalt lijst met productid snack op
                }
                while (selectedMenuItem == "[     Drankjes     ]")
                {
                    Console.Clear();
                    var Drinks = newOrder.GetProducts("Drink");
                    int i = 0;
                    foreach (var Drink in Drinks)
                    {
                        {
                            if (i == index)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"[ drankje : {Drink.Name}, prijs : {Drink.Price} ]");
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine($"[ drankje : {Drink.Name}, prijs : {Drink.Price} ]");

                            }
                            i++;
                            Console.ResetColor();
                        }
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.DownArrow)
                    {
                        if (index == Drinks.Count-1)
                        {
                            index = Drinks.Count-1;
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
                        //newOrder.Add(Drinks[index].ID);
                        resetIndex();
                        Console.Clear();
                        break;
                        // add the ID to an order JSON
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        resetIndex();
                        Console.Clear();
                        break;
                    }

                    // haalt lijst met productid drink op
                }
                while (selectedMenuItem == "[      Alcohol     ]")
                {
                    Console.Clear();
                    var Alcohols = newOrder.GetProducts("Alcohol");
                    int i = 0;
                    foreach (var Alcohol in Alcohols)
                    {
                        {
                            if (i == index)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"[ alcohol: {Alcohol.Name}, prijs : {Alcohol.Price} ]");
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine($"[ alcohol: {Alcohol.Name}, prijs : {Alcohol.Price} ]");
                            }
                            Console.ResetColor();
                            i++;
                        }
                    }

                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.DownArrow)
                    {
                        if (index == Alcohols.Count-1)
                        {
                            index = Alcohols.Count-1;
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
                        //newOrder.Add(Alcohols[index].ID);
                        resetIndex();
                        Console.Clear();
                        break;
                        // add the ID to an order JSON
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        resetIndex();
                        Console.Clear();
                        break;
                    }

                    // haalt lijst met productid alcohol op
                }
                while (selectedMenuItem == "[   Combi Deals    ]")
                {
                    Console.Clear();
                    var Deals = newOrder.GetProducts("Combi");
                    int i = 0;
                    foreach (var Deal in Deals)
                    {
                        {
                            if (i == index)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"[ deal : {Deal.Name}, prijs : {Deal.Price} ]");
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine($"[ deal : {Deal.Name}, prijs : {Deal.Price} ]");
                            }
                            Console.ResetColor();
                            i++;
                        }
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.DownArrow)
                    {
                        if (index == Deals.Count-1)
                        {
                            index = Deals.Count-1;
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
                       // newOrder.Add(Deals[index].ID);
                        resetIndex();
                        Console.Clear();
                        break;
                        // add the ID to an order JSON
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        resetIndex();
                        Console.Clear();
                        break;
                    }
                
                    // haalt lijst met productid combi op

                }

                if (selectedMenuItem == "[       Terug      ]")
                {
                    resetIndex();
                    Console.Clear();
                    productmenubool = false;
                    MainMenu.Mainmenu();
                }
            }  
        }
    }
}