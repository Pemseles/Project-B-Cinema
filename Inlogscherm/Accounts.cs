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
            Console.WriteLine("Uw e-mailadres is gewijzigd!");
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
            Console.WriteLine("Uw wachtwoord is gewijzigd!");
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
                        if (showErrors) { Console.WriteLine("FOUT: Een administratoraccount kan geen VIP worden."); }
                        break;
                    }
                    else if (obj.Level == 2)
                    {
                        if (showErrors) { Console.WriteLine("FOUT: Account heeft al VIP status."); }
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

}