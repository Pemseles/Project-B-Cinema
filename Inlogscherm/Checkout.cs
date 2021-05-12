using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ConsoleApp1
{
    public class Checkout
    {
        private static int index = 0;

        public int FilmIndex;
        public List<int> Products;
        public List<int> Seats;

        public Checkout()
        {
            this.FilmIndex = -1;
            this.Products = new List<int>();
            this.Seats = new List<int>();
        }

        public void UpdateFilmIndex(int index)
        {
            this.FilmIndex = index;
        }
        public void UpdateProducts(List<int> products)
        {
            this.Products = products;
        }
        public void UpdateSeats(List<int> seats)
        {
            this.Seats = seats;
        }

        public void PaymentScreen()
        {
            index = 0;
            bool PaymentScreenBool = true;
            string SelectedOption = "";
            while (PaymentScreenBool)
            {
                List<string> paymentScreenLayout = new List<string>()
                {
                    "[ Keuze aanpassen ]" ,
                    "[    Afrekenen    ]" ,
                    "[   VIP Upgrade   ]" ,
                    "[      Terug      ]"
                };

                SelectedOption = PaymentScreenSelect(paymentScreenLayout);

                if (SelectedOption == "[      Terug      ]")
                {
                    PaymentScreenBool = false;
                }
            }
            Console.Clear();
            MainMenu.Mainmenu();
        }

        private string PaymentScreenSelect(List<string> items)
        {
            Console.Clear();
            DrawCart();
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
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index < 3)
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
                return items[index];
            }
            return "";
        }

        private void DrawCart()
        {
            string cartString = "                                 [        Dit is uw huidige winkelwagen         ]\n";
            cartString = cartString + "                                 [                                              ]\n";
            if (this.FilmIndex < 0)
            {
                cartString = cartString + "                                 [       U heeft nog geen film uitgekozen       ]\n";
            }
            else
            {
                cartString = cartString + $"                                [ {MainMenu.filmList.FilmArray[this.FilmIndex].Name} ]\n";
                //cartString = cartString + $"[ {} ]"; voeg hier prijs toe van de film;
            }
            cartString = cartString + "                                 [                                              ]\n";
            if (this.Products.Count < 1)
            {
                cartString = cartString + "                                 [       U heeft geen producten uitgekozen      ]\n";
            }
            else
            {
                foreach (var productItem in this.Products)
                {
                    // vul in wanneer productenlijst bestaat
                }
            }
            cartString = cartString + "                                 [                                              ]\n";
            if (this.Seats.Count < 1)
            {
                cartString = cartString + "                                 [       U heeft geen stoelen gereserveerd      ]\n";
            }
            else
            {
                foreach (var reservedSeat in this.Seats)
                {
                    // vul in wanneer dit kan
                }
            }
            Console.WriteLine(cartString);
        }
    }
}