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
        public List<Movie> FilmList;

        public Checkout()
        {
            this.FilmIndex = -1;
            this.Products = new List<int>();
            this.Seats = new List<int>();
            this.FilmList = new List<Movie>();
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
        public void UpdateFilmList(List<Movie> newlist)
        {
            this.FilmList = newlist;
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
            string tempCartStr = "";
            string cartString = "                    [                     Dit is uw huidige winkelwagen                      ]\n";
            cartString = cartString + "                    [                                                                        ]\n";
            if (this.FilmIndex < 0 || this.FilmList == null)
            {
                cartString = cartString + "                    [                    U heeft nog geen film uitgekozen                    ]\n";
            }
            else
            {
                // print de naam en tijden van de film
                tempCartStr = BuildCartIndent("Film", -1);
            }
            cartString = cartString + tempCartStr;
            tempCartStr = "";
            cartString = cartString + "                    [                                                                        ]\n";
            if (this.Products.Count < 1)
            {
                cartString = cartString + "                    [                    U heeft geen producten uitgekozen                   ]\n";
            }
            else
            {
                // print de volle lijst van producten
                List<Product> allProducts = Orders.GetProducts();
                for (int i = 0; i < allProducts.Count; i++)
                {
                    int currentID = -1;
                    foreach (int productId in this.Products)
                    {
                        if (allProducts[i].ID == productId)
                        {
                            currentID = productId;
                        }
                    }
                    if (allProducts[i].ID == currentID)
                    {
                        cartString = cartString + BuildCartIndent("Product", currentID);
                    }

                }
            }
            cartString = cartString + "                    [                                                                        ]\n";
            if (this.Seats.Count < 1)
            {
                cartString = cartString + "                    [                    U heeft geen stoelen gereserveerd                   ]\n";
            }
            else
            {
                cartString = cartString + "                    [                U heeft de volgende stoelen gereserveerd                ]\n";
                for (int i = 0; i < this.Seats.Count; i++)
                {
                    cartString = cartString + BuildCartIndent("Seats", i);
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
                    Console.WriteLine("\n                              [          U heeft nog geen film uitgekozen         ]");
                    Console.WriteLine("                              [             U kunt nog niet afrekenen             ]\n");
                }
                else
                {
                    if (selectedOption == "[     Contant     ]")
                    {
                        // tel de prijs op als contant geselecteerd is
                    }
                    else
                    {
                        Console.WriteLine("\n                              [     Transactie gelukt, bedankt voor uw aankoop    ]\n");
                    }
                }
            }
            else if (selectedOption == "[ Keuze resetten  ]")
            {
                if (this.FilmIndex < 0 && this.Products.Count < 1 && this.Seats.Count < 1)
                {
                    Console.WriteLine("\n                              [               Uw winkelwagen is leeg              ]\n");
                }
                else
                {
                    ResetChoices();
                }
            }
        }
        private string BuildCartIndent(string itemType, int index)
        {
            string itemInCart = "";
            int cartLength = 72;

            if (itemType == "Film")
            {
                // print de naam en tijden van de film
                itemInCart = $"Filmnaam: {this.FilmList[FilmIndex].MovieName} | Tijden: {this.FilmList[FilmIndex].StartTime}-{this.FilmList[FilmIndex].EndTime}";
            }
            else if (itemType == "Product")
            {
                List<Product> allProducts = Orders.GetProducts();
                itemInCart = $"Product: {allProducts[index].Name} | Prijs: {allProducts[index].Price}";
            }
            else if (itemType == "Seats")
            {
                if (this.Seats[index] < 10)
                {
                    itemInCart = $"Stoelnummer: {this.Seats[index]} ";
                }
                else
                {
                    itemInCart = $"Stoelnummer: {this.Seats[index]}";
                }
            }

            while (itemInCart.Length < cartLength)
            {
                itemInCart = itemInCart + " ";
                if (itemInCart.Length < cartLength)
                {
                    itemInCart = " " + itemInCart;
                }
            }
            itemInCart = "                    [" + itemInCart + "]\n";
            return itemInCart;
        }
    }
}