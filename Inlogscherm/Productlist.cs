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
                    if (index == 8)
                    {
                        index = 0;
                    }
                    else { index++; }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (index <= 0)
                    {
                        index = 8;
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
            
            public static void Productmenu()
            {
                bool productmenubool = true;
                while (productmenubool == true)
                {
                    List<string> ProductMenu = new List<string>() {
                "[      Snacks      ]" ,
                "[     Drankjes     ]" ,
                "[   Combi Deals    ]" ,
                "[       Terug      ]"
                 };
                    // kijkt bij welke index de user zich bevind
                    string selectedMenuItem = MainScreen(ProductMenu);
                    if (selectedMenuItem == "[      Snacks      ]")
                    {
                        Console.Clear();
                        Console.WriteLine("Dit is de snack lijst");
                        back();
                        // haalt lijst met productid food op
                    }
                    else if (selectedMenuItem == "[     Drankjes     ]")
                    {
                        Console.Clear();
                        Console.WriteLine("Dit is de drankjes lijst");
                        back();
                        // haalt lijst met productid drink op
                    }
                    else if (selectedMenuItem == "[   Combi Deals    ]")
                    {
                        Console.Clear();
                        Console.WriteLine("Dit is de lijst met combinatie deals");
                        back();
                        // haalt lijst met productid deals op
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