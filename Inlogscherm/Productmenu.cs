// productid can be food , drink or deal
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using static ConsoleApp1.MainMenu;
using static ConsoleApp1.Registers;

namespace ConsoleApp1{
public class Products
    {
        public string productsPath = Path.GetFullPath(@"ProductList.json");        
        public static ConsoleApp1.Orders newOrder = new Orders();
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
                if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter)
                {

                }
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (index == 4)
                    {
                        index = 0;
                    }
                    else { index++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        index = 4;
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

            public static void back()
            {
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    Productmenu();
                }
            }
            


        // to do:    zorgen dat het loopt door de opties en je t kan selecteren
        //           wanneer er iets geselcteerd wordt zorgen dat het in een Orders JSON opeslagen wordt
            public static void Productmenu()
            { // Euroteken: 20AC

                bool productmenubool = true;
                while (productmenubool == true)
                {
                
                    List<string> ProductMenu = new List<string>() {
                        "[      Snacks      ]" ,
                        "[     Drankjes     ]" ,
                        "[      Alcohol     ]" ,
                        "[   Combi Deals    ]" ,
                        "[       Terug      ]"
                    };
                    // kijkt bij welke index de user zich bevind
                    string selectedMenuItem = MainScreen(ProductMenu);
                if (selectedMenuItem == "[      Snacks      ]")
                {
                    Console.Clear();
                    var Snacks = newOrder.GetProducts("Snack");
                    foreach(var Snack in Snacks)
                    {
                        Console.WriteLine($"[ snack : {Snack.Name},  prijs : {Snack.Price}] ");
                    }

                    back();
                    // haalt lijst met productid snack op
                }
                else if (selectedMenuItem == "[     Drankjes     ]")
                {
                    Console.Clear();
                    var Drinks = newOrder.GetProducts("Drink");
                    foreach (var Drink in Drinks)
                    {
                        Console.WriteLine($"[ drankje : {Drink.Name}, prijs : {Drink.Price} ]");
                    }

                    back();
                    // haalt lijst met productid drink op
                }
                else if (selectedMenuItem ==  "[      Alcohol     ]")
                {
                    Console.Clear();
                    var Alcohols = newOrder.GetProducts("Alcohol");
                    foreach (var Alcohol in Alcohols)
                    {
                        Console.WriteLine($"[ alcohol: {Alcohol.Name}, prijs : {Alcohol.Price} ]");
                    }

                    back();
                    // haalt lijst met productid alcohol op
                }
                else if (selectedMenuItem == "[   Combi Deals    ]")
                {
                    Console.Clear();
                    var Deals = newOrder.GetProducts("Combi");
                    foreach (var Deal in Deals)
                    {
                        Console.WriteLine($"[ deal : {Deal.Name}, prijs : {Deal.Price} ]");
                    }

                    back();
                    // haalt lijst met productid combi op
                }
                else if (selectedMenuItem == "[       Terug      ]")
                {
                    Console.Clear();
                    MainMenu.Mainmenu();
                }
                    
               
                }


            }
    }
}