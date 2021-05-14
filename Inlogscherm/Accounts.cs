﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace ConsoleApp1 {

    public class Account
    {
        public int UID { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public List<string> Interests { get; set; }
    }


    public class Accounts
    {
        public string Age;
        public string Email;
        public string Pwd;
        public string Firstname;
        public string Lastname;
        public string Address;
        public List<string> Interests;
        public string accountPath = Path.GetFullPath(@"Accounts.json");

        // Generate new UID - Method
        public int GenerateID(){
            int uid = 0;
            try {
                var jsonData = System.IO.File.ReadAllText(accountPath);
                var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

                foreach (Account element in accountsList) { uid = element.UID; };
                return uid + 1;
            }
            catch (Exception) // Excute if there are no entries in the JSON
            {
                return 0;
            }
            
            }

        // INSERT Acount to JSON - Method
        public void AddAccount(string email, string pwd, string firstname, string lastname, string age, string address, string[] interests)
        {
            // Convert Array to List
            List<string> interestLists = interests.ToList();

            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(accountPath);
            // De-serialize to object or create new list
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData)
                                  ?? new List<Account>();

            // Add any new employees
            accountsList.Add(new Account()
            {
                UID = GenerateID(),
                Level = 1,
                Email = email,
                Pwd = pwd,
                Firstname = firstname,
                Lastname = lastname,
                Age = age,
                Address = address,
                Interests = interestLists
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
            System.IO.File.WriteAllText(accountPath, jsonData);

            // serialize JSON to a string and then write string to a file
        }

        // Login - Method
        public int Login(string email, string pwd)
        {
            /// Returns the ID of the User, if incorrect Parameters, given return -1
            int uid = -1;

            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach(Account obj in accountsList)
            {
                // Check if email and pwd are equal
                if(obj.Email == email && obj.Pwd == pwd)
                {
                    // Account Found
                    uid = obj.UID;
                    break;

                }
            }
            return uid;
        }

        // Check if VIP Upgrade is possible - Method
        public bool CheckValidUprade(int uid)
        {
            bool valid = false;

            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach (Account obj in accountsList)
            {
                if (uid == obj.UID)
                {
                    if (obj.Level > 2) {
                        Console.WriteLine("FOUT: Een Administrator Account kan geen VIP worden");
                        break;
                    } 
                    else if (obj.Level == 2)
                    {
                        Console.WriteLine("FOUT: Account heeft al VIP status");
                    } 
                    else
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }

        // Upgrade to VIP - Method
        public void UpgradeToVip(int uid)
        {
            if (CheckValidUprade(uid)) 
            {
                var jsonData = System.IO.File.ReadAllText(accountPath);
                var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);
                foreach (Account obj in accountsList)
                {
                    if (uid == obj.UID)
                    {
                        obj.Level = 2;
                    }
                }
                jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
                System.IO.File.WriteAllText(accountPath, jsonData);
            }
        }
       
    } // ./ Class

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
        public int Active { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }


    public class Orders
    {
        public string orderPath = Path.GetFullPath(@"Orders.json");
        public string accountPath = Path.GetFullPath(@"Accounts.json");
        public string productsPath = Path.GetFullPath(@"ProductList.json");

        // Get Current Seats - Methods
        public int[] GetSeatCoords(int movieID)
        {
            var jsonData = System.IO.File.ReadAllText(orderPath);
            var orderList = JsonConvert.DeserializeObject<List<Order>>(jsonData);

            // Determine the size of the array:
            int index = 0;
            foreach (Order order in orderList)
            {
                if(movieID == order.MovieID)
                {
                    foreach(int seat in order.SeatCoords)
                    {
                        index++;
                    }
                }
            }

            int[] SeatCoords = new int[index];
            index = 0;
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
    }
}

/*==================================================================================================================== *
 * 
 public class Accounts {
    public string Age;
    public string Email; 
    public string Pwd;
    public string Firstname;
    public string Lastname;
    public string Address;
    public List<string> Interests;
    public string accountPath = Path.GetFullPath(@"Accounts.json");

    // Load accounts.json - Method
    public void DeserilizeAccounts()
    {
        string jsonString = File.ReadAllText(accountPath);

        var accountsSerialized = JsonSerializer.Deserialize<NewAccount>(jsonString);
        Console.Write(accountsSerialized);

    }

    // INSERT Acount to JSON - Method
    public void AddAccount(string email, string pwd, string firstname, string lastname, string age, string address, string[] interests) {

        string jsonString = File.ReadAllText(accountPath);



        List<string> interestLists = interests.ToList();
        NewAccount NewUser = new NewAccount
        {
            UID = 3,
            Level = 1,
            Email = email,
            Pwd = pwd,
            Firstname = firstname,
            Lastname = lastname,
            Age = age,
            Address = address,
            Interests = interestLists

        };


        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true
        };



        string newJsonString = JsonSerializer.Serialize(NewUser, options);

        string newJsonnewJsonString = jsonString + ',' + newJsonString;
        Console.Write(newJsonString);
        File.WriteAllText(accountPath, newJsonString);

        // serialize JSON to a string and then write string to a file

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

        public List<Product> GetProducts(string type)
        {
            /// Returns an Int Array of taken seatnumbers
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            List<Product> myProducts = new List<Product>();

            // Determine the size of the array:
            foreach (Product product in productList)
            {
                if(type == product.Type)
                {
                    myProducts.Add(product);
                }
            }
           
            return myProducts;
        }
    }
} // ./ Class

}
*/
// Dag: ___ Maand: ___ Jaar: ___

// Input - Username/Email AND Password
// Output - UID (User ID)
// Load JSON
/*
 public void RetrieveAccounts()
 {
     string AccountPath = Path.GetFullPath(@"Accounts.json");
     string AccountsList = File.ReadAllText(AccountPath);
     ConsoleApp1.NewAccount loginData = new ConsoleApp1.NewAccount();
     loginData = JsonSerializer.Deserialize<ConsoleApp1.NewAccount>(AccountsList);
     loginData.Accounts[1].Age = "13/04/2980";
 }
 */