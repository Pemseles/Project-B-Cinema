using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class Account
    {
        public int UID { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public bool Active { get; set; }
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
        public bool Active;
        public string Firstname;
        public string Lastname;
        public string Address;
        public List<string> Interests;
        public static string accountPath = Path.GetFullPath(@"Accounts.json");

        // Retrieve Accountdata
        public static List<Account> RetrieveAccountData()
        {
            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            return accountsList;
        }

        public static void InsertAccountData(List<Account>AccountData)
        { /// Takes a list of AccountLists
            // Update json data string
            var jsonData = JsonConvert.SerializeObject(AccountData, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(accountPath, jsonData);
        }

        // Generate new UID - Method
        public int GenerateID()
        {
            /// Checks Accounts.json and creates a new ID based on previous ID's from the Json.
            int uid = 0;
            try
            {
                var accountsList = RetrieveAccountData();

                foreach (Account element in accountsList) { uid = element.UID; };
                return uid + 1;
            }
            catch (Exception) // Execute if there are no entries in the JSON
            {
                return 0;
            }
        }

        // Encrypt String - Method
        public string Encrypt(string pwd)
        {
            /// Hashes new Password and Convert to String (for JSON);
            PasswordHash hash = new PasswordHash(pwd);
            byte[] hashBytes = hash.ToArray();

            // Store in String
            string hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }

        // INSERT Acount to JSON - Method
        public void AddAccount(string email, string pwd, string firstname, string lastname, string age, string address, string[] interests, int level=1)
        {
            /// Requires All and only Correct Parameters to Insert to the JSON File.
            // Convert Array to List
            List<string> interestLists = interests.ToList();

            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(accountPath);
            // De-serialize to object or create new list
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData)
                                  ?? new List<Account>();

            // Hashing the Password
            string hashedPwd = Encrypt(pwd);

            // Add any new employees
            accountsList.Add(new Account()
            {
                UID = GenerateID(),
                Level = level, // Default 1
                Email = email,
                Pwd = hashedPwd,
                Active = true,
                Firstname = firstname,
                Lastname = lastname,
                Age = age,
                Address = address,
                Interests = interestLists
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(accountPath, jsonData);
        }

        // Login - Method
        public int Login(string email, string pwd)
        {
            /// Returns the ID of the User, if incorrect Parameters given: return -1
            int uid = -1;

            var accountsList = RetrieveAccountData();

            foreach (Account obj in accountsList)
            {
                // Look for a matching Email
                if (obj.Email == email)
                { // Email is Correct!
                    // Convert back to Byte Array
                    byte[] hashBytes = Convert.FromBase64String(obj.Pwd);
                    PasswordHash hash = new PasswordHash(hashBytes);
                    if (hash.Verify(pwd))
                    {
                        // Password is Valid! Return User ID
                        uid = obj.UID;
                        break;
                    }
                }
            }
            return uid;
        }

        // Get Level - Method
        public int GetLevel(int uid)
        {
            /// Takes UID as Parameter, Returns User Clearance Level
            if (uid < 0) return 1; 
            var accountsList = RetrieveAccountData();
            int level = 0;
            foreach (Account user in accountsList)
            {
                if (uid == user.UID)
                {
                    // Set bool to false and break loop
                    level = user.Level;
                    break;
                }
            }
            return level;

        }

        public bool GetActiveStatus(int uid)
        {
            /// Takes UID as Parameter, Returns User Clearance Level
            bool active = false;
            if (uid < 0) return active;
            var accountsList = RetrieveAccountData();
           
            foreach (Account user in accountsList)
            {
                if (uid == user.UID)
                {
                    // Set bool to false and break loop
                    active = user.Active;
                    break;
                }
            }
            return active;
        }

        public bool CheckUniqueEmail(string email)
        {
            /// Takes string email as Parameter, Returns Boolean, whether Email is Unique or not.
            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            bool UniqueEmail = true;

            // Unique till proven otherwise:
            foreach (Account user in accountsList)
            {
                if (email == user.Email)
                {
                    // Set bool to false and break loop
                    UniqueEmail = false;
                    break;
                }
            }
           return UniqueEmail;
        }

        public void UpdateEmail(int uid, string newEmail)
        {
            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach (Account user in accountsList)
            {
                if (uid == user.UID)
                {
                    user.Email = newEmail;
                }
            }
            jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
            System.IO.File.WriteAllText(accountPath, jsonData);
            Console.WriteLine("Je Email Adres is Gewijzigd!");
        }

        public void UpdatePwd(int uid, string newPwd)
        {
            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach (Account user in accountsList)
            {
                if (uid == user.UID)
                {
                    user.Pwd = Encrypt(newPwd);
                }
            }

            jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
            System.IO.File.WriteAllText(accountPath, jsonData);
            Console.WriteLine("Je Wachtwoord is Gewijzigd!");
        }

        // Check if VIP Upgrade is possible - Method
        public bool CheckValidUprade(int uid, bool showErrors = false)
        {
            bool valid = false;

            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach (Account obj in accountsList)
            {
                if (uid == obj.UID)
                {
                    if (obj.Level > 2)
                    {
                        if (showErrors) { Console.WriteLine("FOUT: Een Administrator Account kan geen VIP worden"); }
                        break;
                    }
                    else if (obj.Level == 2)
                    {
                        if (showErrors) { Console.WriteLine("FOUT: Account heeft al VIP status"); }
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
            if (CheckValidUprade(uid, true))
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

    public abstract class  Utility
    {
        // Database Paths:
        public string orderPath = Path.GetFullPath(@"Orders.json");
        public string accountPath = Path.GetFullPath(@"Accounts.json");
        public static string productsPath = Path.GetFullPath(@"ProductList.json");
        public string theatherhallPath = Path.GetFullPath(@"Theaterhalls.json");
        public string filmPath = Path.GetFullPath(@"Films.json");
        public string filmInstancesPath = Path.GetFullPath(@"FilmInstances.json");

        public static List<Object> retrieveJson(string jsonPath)
        {
            /// Retrieves a Json Data and converts it to an Object List
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var objectList = JsonConvert.DeserializeObject<List<Object>>(jsonData);

            return objectList;
        }

        public static void InsertJson(List<Object> objectList, string jsonPath)
        {
            // Update json data string
            var jsonData = JsonConvert.SerializeObject(objectList, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(jsonPath, jsonData);
        }

        public int GenerateID(string JsonPath)
        {
            /// Checks Accounts.json and creates a new ID based on previous ID's from the Json.
            int newID = 0;
            try
            {
                var objectList = retrieveJson(JsonPath);

                foreach (Product element in objectList) { newID = element.ID; };
                return newID + 1;
            }
            catch (Exception) // Execute if there are no entries in the JSON
            {
                return 0;
            }
        }
    }

    public class Admin : Utility
    {
        private int AdminID;
        public Admin(int UID) { this.AdminID = UID; }

        public void AddNewFilm(string filmName, string director, string[] genres, int ageRating)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var filmList = JsonConvert.DeserializeObject<List<Film>>(jsonData) ?? new List<Film>();

            // Add a new Film
            filmList.Add(new Film()
            {
                ID = GenerateID(filmPath),
                Name = filmName,
                Director = director,
                Genres = genres,
                AgeRating = ageRating
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(filmList, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(filmPath, jsonData);
        }

        public void AddFilmInstance(int movieID, int theaterhallID, string startDateTime, string endDateTime, string type, decimal price)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var filmInstances = JsonConvert.DeserializeObject<List<Filminstance>>(jsonData) ?? new List<Filminstance>();

            // Add a new Film
            filmInstances.Add(new Filminstance()
            {
                ID = GenerateID(filmInstancesPath),
                MovieID = movieID,
                TheaterhallID = theaterhallID,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Type = type,
                Price = price,
                Active = true
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(filmInstances, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(filmPath, jsonData);
        }

        public void RemoveFilmInstance(int id)
        {
            /// Takes Int ID as Parameter to locate & Remove Entry with Matching ID
            var FilmInstances = retrieveJson(filmInstancesPath);
            int index = 0;
            foreach(Filminstance filmInstance in FilmInstances)
            {
                if(id == filmInstance.ID)
                    break;
                index++;
            }

            // Remove entry and update file
            FilmInstances.RemoveAt(index);
            InsertJson(FilmInstances, filmInstancesPath);
        }

        public void RemoveFilm(int id)
        {
            /// Takes Int ID as Parameter to locate & Remove Parent + Child Entry with Matching ID.
            var FilmList = retrieveJson(filmPath);
            var FilmInstances = retrieveJson(filmInstancesPath);

            int index = 0;
            // Locate and Remove all Child Entries
            foreach(Filminstance filmInstance in FilmList)
            {
                if (id == filmInstance.MovieID) {
                    FilmInstances.RemoveAt(index);
                } else {
                    index++;
                }
            }

            index = 0;
            foreach (Film film in FilmList)
            {
                if (id == film.ID)
                {
                    break;
                }
                index++;
            }

            // Remove entry and update files
            FilmList.RemoveAt(index);
            InsertJson(FilmList, filmPath);
            InsertJson(FilmInstances, filmInstancesPath);
        }

        public void AddTheatherhall(string hallName, int numberOfSeats, List<int> vipSeatCoords, bool active=true)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var theatherhallList = JsonConvert.DeserializeObject<List<Theaterhall>>(jsonData) ?? new List<Theaterhall>();

            // Add a new Film
            theatherhallList.Add(new Theaterhall()
            {
                ID = GenerateID(theatherhallPath),
                Name = hallName,
                NumberOfSeats = numberOfSeats,
                VipSeatCoords = vipSeatCoords,
                Active = active
            });
            // Update json data string
            jsonData = JsonConvert.SerializeObject(theatherhallList, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(theatherhallPath, jsonData);
        }

        public void SuspendAccount(int UID)
        { /// Suspends the Account where ID matches with given int Parameter
            List<Object> AccountList = retrieveJson(accountPath);

            foreach(Account Account in AccountList)
            {
                if(UID == Account.UID)
                {
                    Account.Active = false;
                    break;
                }
            }
        }

        public List<Account> GetAllSuspendedAccounts()
        { /// Lists all Suspended Acdcounts
            List<Account> AccountList = Accounts.RetrieveAccountData();
            List<Account> SuspendedAccounts = new List<Account>();
            foreach(Account account in AccountList)
            {
                if (account.Active == false)
                {
                    SuspendedAccounts.Add(account);
                }
            }
            return SuspendedAccounts;
        }
    }

    public class Orders : Utility
    {
        // Get Current Seats - Methods
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

        // Get VIP Seats
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

        // overload 1, takes string type as param, (for use in Productmenu.cs)
        public List<Product> GetProducts(string type)
        {
            /// Takes a string Type as Paramater and creates a list of all products of given Type:
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            List<Product> myProducts = new List<Product>();

            foreach (Product product in productList)
            {
                if(type == product.Type)
                {
                    myProducts.Add(product);
                }
            }
            return myProducts;
        }
        // overload 2, has no params, (for use in Checkout.cs)
        public static List<Product> GetProducts()
        {
            /// Takes no Paramaters and creates a list of all products registered in json (used in Checkout.cs):
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            List<Product> myProducts = new List<Product>();

            foreach (Product product in productList)
            {
                myProducts.Add(product);
            }
            return myProducts;
        }
    }

    public sealed class PasswordHash // *Sealed Means that his Class cannot be inherited by other Classes
    {
        // Delcaring Sizes
        const int SaltSize = 16, HashSize = 20, HashIter = 10000; // Const Makes the variable immutable and fixed. 
        readonly byte[] _salt, _hash;
        /* Salt is used to add an extra string of hashed characters to a hashed password to make it less sensitve to both Dictionary & BruteForce Attacks */
        public PasswordHash(string password)
        {
            // Converts the Password to bytes using various build-in crypto classes
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }
        public PasswordHash(byte[] hashBytes)
        {
            // Generates salt and adds to array
            Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
        }
        public PasswordHash(byte[] salt, byte[] hash)
        {
            // Hard Copy
            Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
        }
        public byte[] ToArray()
        {
            // Combines the hash and salt and converts it to a byte array
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }
        public byte[] Salt { get { return (byte[])_salt.Clone(); } }
        public byte[] Hash { get { return (byte[])_hash.Clone(); } }
        public bool Verify(string password)
        {
            // Checks if given password is valid by comparing it to a hashed password
            byte[] test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
                if (test[i] != _hash[i])
                    return false;
            return true;
        }
    }
}