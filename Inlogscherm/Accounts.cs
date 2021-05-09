using System;
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

            var jsonData = System.IO.File.ReadAllText(accountPath);
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

            foreach (var element in accountsList) { uid = element.UID; };

            return uid + 1;
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