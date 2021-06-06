using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;

namespace ConsoleApp1
{
    public class Checkout
    {
        private static int index = 0;
        public decimal FinalPrice = 0.0M;

        public List<int> Products;
        public List<int> Seats;
        public Filminstance FilmPrice;
        public string FilmName;
        public string FilmStartTime;
        public string FilmEndTime;

        public Checkout()
        {
            this.Products = new List<int>();
            this.Seats = new List<int>();
            this.FilmPrice = null;
            this.FilmName = "";
            this.FilmStartTime = "";
            this.FilmEndTime = "";
        }

        public void UpdateProducts(List<int> products)
        {
            this.Products = products;
        }
        public void UpdateSeats(List<int> seats)
        {
            this.Seats = seats;
        }
        public void UpdateFilmPrice(Filminstance newSelection)
        {
            this.FilmPrice = newSelection;
        }
        public void UpdateFilmName(string newName)
        {
            this.FilmName = newName;
        }

        public void UpdateFilmStartnEnd(string start, string end)
        {
            this.FilmStartTime = start;
            this.FilmEndTime = end;
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
                    "[      Terug      ]"
                };

                SelectedOption = PaymentScreenSelect(paymentScreenLayout, SelectedOption);
                if (SelectedOption == "[    Afrekenen    ]")
                {
                    Console.Clear();
                    CheckoutScreen();

                }
                else if (SelectedOption == "[      Terug      ]")
                {
                    PaymentScreenBool = false;
                }
            }
            Console.Clear();
            if (Program.Level >= 3)
            {
                AdminMenu.Mainmenu();
            }
            else if (Program.UID == -1)
            {
                Guest.Mainmenu();
            }
            else
            {
                MainMenu.Mainmenu();
            }
        }

        private string PaymentScreenSelect(List<string> items, string SelectedItem)
        {
            GetTotalPrice();
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
                if (index < 2)
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
            string secondtempCartStr = "";
            string cartString = "                    [                     Dit is uw huidige winkelwagen                      ]\n";
            cartString = cartString + "                    [                                                                        ]\n";
            if (this.FilmPrice == null)
            {
                cartString = cartString + "                    [                    U heeft nog geen film uitgekozen                    ]\n";
            }
            else
            {
                // print de naam en tijden van de film
                tempCartStr = BuildCartIndent("Film", -1);
                secondtempCartStr = BuildCartIndent("Film2", -1);
            }
            cartString = cartString + tempCartStr;
            cartString = cartString + secondtempCartStr;
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
            if (this.FinalPrice > 0.0M)
            {
                cartString = cartString + "                    [   ------------------------------------------------------------------   ]\n";
                cartString = cartString + BuildCartIndent("Price", -1);
            }
            Console.WriteLine(cartString);
        }

        private void ResetChoices()
        {
            if (this.Products.Count > 0)
            {
                UpdateProducts(new List<int>());
            }
            if (this.Seats.Count > 0)
            {
                UpdateSeats(new List<int>());
            }
            if (this.FilmPrice != null)
            {
                UpdateFilmPrice(null);
                UpdateFilmName("");
                UpdateFilmStartnEnd("", "");
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
                if (this.FilmPrice != null)
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
                if (this.Products.Count < 1 && this.Seats.Count < 1 && this.FilmPrice == null)
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
                itemInCart = $"Filmnaam: {this.FilmName}";
                
            }
            else if (itemType == "Film2")
            {
                itemInCart = $"Tijden: {this.FilmStartTime}-{this.FilmEndTime} | Ticketprijs: {this.FilmPrice.Price}";
            }
            else if (itemType == "Product")
            {
                List<Product> allProducts = Orders.GetProducts();
                itemInCart = $"Product: {allProducts[index].Name} | Prijs: {allProducts[index].Price}";
            }
            else if (itemType == "Seats")
            {
                char[] Alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
                string seatStr = "";
                seatStr = $"{Alphabet[this.Seats[index] / 10]}{this.Seats[index] % 10 + 1}";
                if (seatStr.Length <= 2)
                {
                    itemInCart = $"Stoelnummer: {seatStr} ";
                }
                else
                {
                    itemInCart = $"Stoelnummer: {seatStr}";
                }
            }
            else if (itemType == "Price")
            {
                itemInCart = $"Totaalprijs: {this.FinalPrice}";
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

        private void GetTotalPrice()
        {
            decimal newPrice = 0.0M;
            List<Product> allProducts = Orders.GetProducts();

            for (int i = 0; i < allProducts.Count; i++)
            {
                foreach (var productId in this.Products)
                {
                    if (allProducts[i].ID == productId)
                    {
                        newPrice = newPrice + allProducts[i].Price;
                    }
                }
            }
            if (this.FilmPrice != null)
            {
                newPrice = newPrice + this.FilmPrice.Price;
            }
            this.FinalPrice = newPrice;
        }
    }
}