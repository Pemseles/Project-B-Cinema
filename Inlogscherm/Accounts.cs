using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
//using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1 {
    

    public class NewAccount {
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


    public class Accounts {
        public string Age;
        public string Email; 
        public string Pwd;
        public string Firstname;
        public string Lastname;
        public string Address;
        public List<string> Interests;

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

        public void AddAccount(string email, string pwd, string firstname, string lastname, string age, string address, string[] interests) {

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

            // serialize JSON to a string and then write string to a file
            string AccountPath = Path.GetFullPath(@"Accounts.json");
            File.WriteAllText(AccountPath, JsonConvert.SerializeObject(NewUser));
        }
    } // ./ Class

}

// Dag: ___ Maand: ___ Jaar: ___

// Input - Username/Email AND Password
// Output - UID (User ID)