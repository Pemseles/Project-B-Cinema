using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Orders : Utility  // Inherits from Abstract Class
    {
        /// <summary>
        /// Retrieves the amount of seats from a given Hallid
        /// </summary>
        /// <param name="hallID">The ID(int) of the Theaterhall</param>
        /// <returns>The Amount of seats as integer</returns>
        public int GetSeats(int hallID)
        {
            /// Get Amount of seats of given Hall ID
            var jsonData = System.IO.File.ReadAllText(theatherhallPath);
            var theaterhalls = JsonConvert.DeserializeObject<List<Theaterhall>>(jsonData);

            int amountOfSeats = 0;
            foreach (Theaterhall theaterhall in theaterhalls)
            {
                if (hallID == theaterhall.ID)
                {
                    amountOfSeats = theaterhall.NumberOfSeats;
                    break;
                }
            }
            return amountOfSeats;
        }

        /// <summary>
        /// Get Current Seats - Methods
        /// Searches through all the orders that are connected to the selected movie id and collects them in an int array.
        /// </summary>
        /// <param name="movieID">The ID of the movie instance</param>
        /// <returns>An Integer Array with all the coordinates of all the reserved seats.</returns>
        public int[] GetSeatCoords(int movieID)
        {
            /// Returns an Int Array of taken seatnumbers
            var jsonData = System.IO.File.ReadAllText(orderPath);
            var orderList = JsonConvert.DeserializeObject<List<Order>>(jsonData);

            // Determine the size of the array:
            int index = 0;
            foreach (Order order in orderList)
            {
                if (movieID == order.MovieID)
                {
                    foreach (int seat in order.SeatCoords)
                    {
                        index++;
                    }
                }
            }
            int[] SeatCoords = new int[index];
            index = 0;
            // Loop through Orders again and locate all seat coords.
            foreach (Order order in orderList)
            {
                if (movieID == order.MovieID)
                {
                    foreach (int seat in order.SeatCoords)
                    {
                        SeatCoords[index] = seat;
                        index++;
                    }
                }
            }
            return SeatCoords;
        }

        /// <summary>
        /// Get VIP Seats
        /// Takes the HallID as Parameter and gets all VIP seats
        /// </summary>
        /// <param name="TheatherHallID">The ID(int) of the Theatherhall</param>
        /// <returns></returns>
        public int[] GetVipSeatCoords(int TheatherHallID)
        {
            /// Returns an Int Array of taken seatnumbers
            var jsonData = System.IO.File.ReadAllText(theatherhallPath);
            var theater = JsonConvert.DeserializeObject<List<Theaterhall>>(jsonData);

            // Determine the amount of Vip Seats
            int index = 0;
            foreach (Theaterhall hall in theater)
            {
                if (hall.ID == TheatherHallID)
                {
                    foreach (int VipSeat in hall.VipSeatCoords)
                    {
                        index++;
                    }
                }
            }
            // Create an int array
            int[] VipSeatCoords = new int[index];
            index = 0;

            // Add the Vip seatcoords to an Int Array
            foreach (Theaterhall hall in theater)
            {
                if (hall.ID == TheatherHallID)
                {
                    foreach (int VipSeat in hall.VipSeatCoords)
                    {
                        VipSeatCoords[index++] = VipSeat;
                    }
                }
            }
            return VipSeatCoords;
        }

        // Overload 1, takes string type as param, (for use in Productmenu.cs)
        /// <summary>
        /// Overload 1, takes string type as param, (for use in Productmenu.cs)
        /// Lists all product that match given type
        /// </summary>
        /// <param name="type">Product type (string)</param>
        /// <returns>A list with the Filtered Products</returns>
        public List<Product> GetProducts(string type)
        {
            /// Takes a string Type as Paramater and creates a list of all products of given Type:
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            List<Product> myProducts = new List<Product>();

            foreach (Product product in productList)
            {
                if (type == product.Type)
                {
                    myProducts.Add(product);
                }
            }
            return myProducts;
        }
        // Overload 2, has no params, (for use in Checkout.cs)
        /// <summary>
        /// Overload 2, has no params, (for use in Checkout.cs)
        /// Lists all Products
        /// </summary>
        /// <returns>A list with all Products</returns>
        public static List<Product> GetProducts()
        {
            /// Takes no Paramaters and creates a list of all products registered in json (used in Checkout.cs):
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            return productList;
        }
    }

    // All Objects (mainly) needed for Orders
    public class Order
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public int MovieID { get; set; }
        public string ordernumber { get; set; }
        public List<int> SeatCoords { get; set; }
        public List<int> ProductIDs { get; set; }
    }

    public class Theaterhall
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public List<int> VipSeatCoords { get; set; }
        public bool Active { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}