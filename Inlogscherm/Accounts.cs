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
        /// <summary>
        /// Retrieve Accountdata
        /// </summary>
        /// <returns>A List with all the Accounts (Object = Account)</returns>
        public static List<Account> RetrieveAccountData()
        {
            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            return accountsList;
        }

        /// <summary>
        /// Inserts a List with the new Account Data.
        /// (Serialized to the Accounts.json)
        /// </summary>
        /// <param name="AccountData">An Update List of the Accounts</param>
        public static void InsertAccountData(List<Account>AccountData)
        { /// Takes a list of AccountLists
            // Update json data string
            var jsonData = JsonConvert.SerializeObject(AccountData, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(accountPath, jsonData);
        }

        /// <summary>
        ///  Generate new UID - Method
        /// </summary>
        /// <returns>A new Generated ID (int) for a new account</returns>
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
        /// <summary>
        /// Encrypt String - Method
        /// Encrypts the paramater(string) using hash.
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns>Hashed String</returns>
        public string Encrypt(string pwd)
        {
            /// Hashes new Password and Convert to String (for JSON);
            PasswordHash hash = new PasswordHash(pwd);
            byte[] hashBytes = hash.ToArray();

            // Store in String
            string hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }

        /// <summary>
        /// INSERT Account to JSON - Method
        /// Insert to Accounts.json
        /// </summary>
        /// <param name="email">User's Email</param> 
        /// <param name="pwd">Unhashed Version of the Password (Build-in Hash function)</param> 
        /// <param name="firstname"> User's Firstname</param>
        /// <param name="lastname">User's Lastname</param>
        /// <param name="age">Birthdate(string)</param>
        /// <param name="address">Address</param>
        /// <param name="interests">String Array with the Interests</param>
        /// <param name="level">Access Level - Default 1</param>
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

        /// <summary>
        /// Login - Method
        /// Requires 2 strings Email & Password
        /// </summary>
        /// <param name="email">String Email</param>
        /// <param name="pwd">String Password</param>
        /// <returns>Returns the ID of the User, if incorrect Parameters given: return -1</returns>
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

        /// <summary>
        /// Get Level - Method
        /// </summary>
        /// <param name="uid">Takes User ID as Parameter</param>
        /// <returns>Returns User Clearance Level</returns>
        public int GetLevel(int uid)
        { /// Takes UID as Parameter, Returns User Clearance Level
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

        /// <summary>
        /// Tuple - Method
        /// A method to get User's Fullname
        /// </summary>
        /// <param name="uid">Takes UID as Parameter</param>
        /// <returns>Returns a Tuple with First and Lastname</returns>
        public static Tuple<string,string> GetFullname(int uid)
        {
            /// Takes UID as Parameter, Returns a Tuple with First and Lastname
            if (uid < 0) return Tuple.Create("Anoniem", "");
            var accountsList = RetrieveAccountData();
            string firstname = "";
            string lastname  = "";

            foreach (Account user in accountsList)
            {
                if (uid == user.UID)
                {
                    // Set bool to false and break loop
                    firstname = user.Firstname;
                    lastname  = user.Lastname;
                    break;
                }
            }
            return Tuple.Create(firstname, lastname);
        }

        /// <summary>
        /// Get the Active Status of an Account
        /// Takes UID as Parameter, Returns a Bool whether the account is active or not
        /// </summary>
        /// <param name="uid">Takes the User ID as Parameter</param>
        /// <returns>A Bool whether the account is active or not</returns>
        public bool GetActiveStatus(int uid)
        {
            /// Takes UID as Parameter, Returns a Bool whether the account is active or not
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

        /// <summary>
        /// Checks wether an email is Unique.
        /// Takes string email as Parameter, Returns Boolean, whether Email is Unique or not.
        /// </summary>
        /// <param name="email">String of the user's Email</param>
        /// <returns> A Boolean whether Email is Unique or not</returns>
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

        /// <summary>
        /// Takes the User's ID and a New Email as Parameters, and Replaces the old email with the new email.
        /// </summary>
        /// <param name="email">String of the user's Email</param>
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

        /// <summary>
        /// Takes the User's ID and a New Email as Parameters, and Replaces the old email with the new email.
        /// </summary>
        /// <param name="email">String of the user's Email</param>
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

        /// <summary>
        /// Checks if a VIP Upgrade is possible - Method
        /// To prevent a VIP Account from upgrading twice and
        /// to Prevent an Admin from Downgrading to VIP.
        /// </summary>
        /// <param name="uid">The ID of the user's account</param>
        /// <param name="showErrors">A Bool for showing errors to the Admin, Default False</param>
        /// <returns>A bool wether the updrade is possible or not.</returns>
        public bool CheckValidUprade(int uid, bool showErrors=false)
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

        /// <summary>
        ///  Upgrade to VIP - Method
        ///  After the check of a valid Vip Upgrade ( CheckValidUpgrade(); ) 
        /// The Clearance level of the user's account will be set to VIP(2).
        /// </summary>
        /// <param name="uid">Takes the User's ID as parameter</param>
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

    public abstract class  Utility // Abstract Class
    {
       // Database Paths:
       protected string orderPath = Path.GetFullPath(@"Orders.json");
       protected string accountPath = Path.GetFullPath(@"Accounts.json");
       protected static string productsPath = Path.GetFullPath(@"ProductList.json");
       protected string theatherhallPath = Path.GetFullPath(@"Theaterhalls.json");
       protected string filmPath = Path.GetFullPath(@"Films.json");
       protected string filmInstancesPath = Path.GetFullPath(@"FilmInstances.json");

        /// <summary>
        /// Retrieves a Json Data and converts it to an Object List
        /// </summary>
        /// <param name="jsonPath">the PAth & Filename(String)</param>
        /// <returns>Object List of given Json File</returns>
        public static List<Object> retrieveJson(string jsonPath)
        {
            /// Retrieves a Json Data and converts it to an Object List
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var objectList = JsonConvert.DeserializeObject<List<Object>>(jsonData);

            return objectList;
        }

        /// <summary>
        /// Inserts an Object List to FilePath
        /// </summary>
        /// <param name="objectList">The objectlist that needs to be Inserted</param>
        /// <param name="jsonPath">The Path & Filename(String)</param>
        public static void InsertJson(List<Object> objectList, string jsonPath)
        {
            // Update json data string
            var jsonData = JsonConvert.SerializeObject(objectList, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(jsonPath, jsonData);
        }

        /// <summary>
        /// Generates an new ID based on the previous entry in the given Json File
        /// </summary>
        /// <param name="JsonPath"></param>
        /// <returns></returns>
        public static int GenerateID(string JsonPath)
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

    public class Admin : Utility // Inherits from Abstract Class
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
            // Then Locate Parent
            foreach (Film film in FilmList)
            {
                if (id == film.ID){ break; }
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
        { /// Lists all Suspended Adcounts
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

    public class ReviewCreation : Utility  // Inherits from Abstract Class
    {
        public static string ReviewPath = Path.GetFullPath(@"Reviews.json");
       
        public static int GenerateID()
        {
            /// Checks opens the json file given as parameter and creates a new ID based on previous ID's from the Json.
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);

            int newID = 0;
            try
            {
                foreach (Review element in reviews) { newID = element.ID; };
                return newID + 1;
            }
            catch (Exception) // Execute if there are no entries in the JSON
            {
                return 0;
            }
        }


        // Add Method
        public static void AddReview(int uid, int tagID, int revID, string author, string title, string description, int stars, string uploadDate)
        {
            /// Requires All and only Correct Parameters to Insert to the JSON File.
            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            // De-serialize to object or create new list
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData)
                                  ?? new List<Review>();
            // Adds a new Review
            reviews.Add(new Review()
            {
                ID = GenerateID(),
                UID = uid, // Default 1
                TagID = tagID,
                RevID = revID,
                Author = author,
                Title = title,
                Description = description,
                Stars = stars,
                UploadDate = uploadDate,
                Active = true,
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            // serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(ReviewPath, jsonData);
        }

        public static List<Review> GetAllReviews()
        {
            /// Deletes a Review, with given ID.
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            return reviews;
        }

        // Delete Method
        public static void DeleteReview(int id)
        {
            /// Deletes a Review, with given ID.
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var reviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in reviews)
            {
                if (id == review.ID)
                {
                    FilteredReviews.Remove(review);
                }
            }

            // Update json data string
            jsonData = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(ReviewPath, jsonData);
        }

        /* Filtering
         * The following methods are for filtering:
         * - Filter on reviews for certain movies.
         * - Filter on users, or just on tags.
         */

        // Param Overload 1 - On both TagID and ID of given Subject
        public static List<Review> FilterReviews(int tagID, int revID)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach(Review review in AllReviews){
                if(tagID == review.TagID && revID == review.RevID) {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }
        // Param Overload 2 - Only Filter on tagID
        public static List<Review> FilterReviews(int tagID)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in AllReviews)
            {
                if (tagID == review.TagID)
                {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }

        // Filter all Reviews of a user.
        public static List<Review> GetUserReviews(int uid)
        { /// Filters all reviews with given parameters
            var jsonData = System.IO.File.ReadAllText(ReviewPath);
            var AllReviews = JsonConvert.DeserializeObject<List<Review>>(jsonData);
            // Create new list with only filtered Reviews
            List<Review> FilteredReviews = new List<Review>();
            foreach (Review review in AllReviews)
            {
                if (uid == review.UID)
                {
                    FilteredReviews.Add(review);
                }
            }
            return FilteredReviews;
        }
    }

    public class Orders : Utility  // Inherits from Abstract Class
    {
        public int GetSeats(int hallID)
        {
            /// Get Amount of seats of given Hall ID
            var jsonData = System.IO.File.ReadAllText(theatherhallPath);
            var theaterhalls = JsonConvert.DeserializeObject<List<Theaterhall>>(jsonData);

            int amountOfSeats = 0;
            foreach(Theaterhall theaterhall in theaterhalls)
            {
                if(hallID == theaterhall.ID)
                {
                    amountOfSeats = theaterhall.NumberOfSeats;
                    break;
                }
            }
            return amountOfSeats;
        }

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

        // Overload 1, takes string type as param, (for use in Productmenu.cs)
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
        // Overload 2, has no params, (for use in Checkout.cs)
        public static List<Product> GetProducts()
        {
            /// Takes no Paramaters and creates a list of all products registered in json (used in Checkout.cs):
            var jsonData = System.IO.File.ReadAllText(productsPath);
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            return productList;
        }
    }

    public sealed class PasswordHash // *Sealed Means that this Class cannot be inherited by other Classes
    {
        // Delcaring Sizes
        // Private Fields Encapsulation
        const int SaltSize = 16, HashSize = 20, HashIter = 10000; // Const Makes the variable immutable and fixed. 
        readonly byte[] _salt, _hash;

        /* Salt is used to add an extra string of hashed characters to a hashed --
         * password to make it less sensitve to both Dictionary & BruteForce Attacks */
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