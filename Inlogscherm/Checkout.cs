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
                    "[ Keuze resetten  ]" ,
                    "[    Afrekenen    ]" ,
                    "[   VIP Upgrade   ]" ,
                    "[      Terug      ]"
                };

                SelectedOption = PaymentScreenSelect(paymentScreenLayout, SelectedOption);
                if (SelectedOption == "[    Afrekenen    ]")
                {
                    Console.Clear();
                    CheckoutScreen();

                }
                else if (SelectedOption == "[   VIP Upgrade   ]")
                {
                    // doe hier VIP upgraden
                }
                else if (SelectedOption == "[      Terug      ]")
                {
                    PaymentScreenBool = false;
                }
            }
            Console.Clear();
            MainMenu.Mainmenu();
        }

        private string PaymentScreenSelect(List<string> items, string SelectedItem)
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
            DrawCheckouts(SelectedItem);
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

        private void ResetChoices()
        {
            if (this.FilmIndex >= 0)
            {
                UpdateFilmIndex(-1);
            }
            if (this.Products.Count > 0)
            {
                UpdateProducts(new List<int>());
            }
            if (this.Seats.Count > 0)
            {
                UpdateSeats(new List<int>());
            }
        }

        public void CheckoutScreen()
        {
            index = 0;
            bool checkoutBool = true;
            string SelectedOption = "";
            while (checkoutBool)
            {
                List<string> checkoutScreenLayout = new List<string>()
                {
                    "[      iDeal      ]" ,
                    "[   Credit card   ]" ,
                    "[     Contant     ]" ,
                    "[      Terug      ]"
                };
                SelectedOption = PaymentScreenSelect(checkoutScreenLayout, SelectedOption);
                
                if (SelectedOption == "[      Terug      ]")
                {
                    checkoutBool = false;
                }
                else
                {
                    DrawCheckouts(SelectedOption);
                }
            }
            Console.Clear();
            PaymentScreen();
        }

        private void DrawCheckouts(string selectedOption)
        {
            if (selectedOption == "[     Contant     ]" || selectedOption == "[      iDeal      ]" || selectedOption == "[   Credit card   ]")
            {
                if (this.FilmIndex < 0)
                {
                    Console.WriteLine("\n                                 [       U heeft nog geen film uitgekozen       ]");
                    Console.WriteLine("                                 [          U kunt nog niet afrekenen           ]\n");
                }
                else
                {
                    if (selectedOption == "[     Contant     ]")
                    {
                        // tel de prijs op als contant geselecteerd is
                    }
                    else
                    {
                        Console.WriteLine("                                 [  Transactie gelukt, bedankt voor uw aankoop  ]\n");
                    }
                }
            }
            else if (selectedOption == "[ Keuze resetten  ]")
            {
                if (this.FilmIndex < 0 && this.Products.Count < 1 && this.Seats.Count < 1)
                {
                    Console.WriteLine("\n                                 [            Uw winkelwagen is leeg            ]\n");
                }
                else
                {
                    ResetChoices();
                }
            }
        }
    }
}