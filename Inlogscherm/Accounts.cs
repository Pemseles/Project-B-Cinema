using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1 {
    

    public class Account {
        public int UID { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string[] Interests { get; set; }
    }


    public class Accounts {
        public string Age;
        public string Email; 
        public string Pwd;
        public string Firstname;
        public string Lastname;
        public string Address;
        public string[] Interests;

        public Account(string email)
        { // Constructor
            this.Email = email;
            this.Age = 0; // Note that if we left this line out, Age would have the same value as 0 is the default value for an int
        }

        // Load JSON
        public void retrieveAccounts()
        {
            this.Age + 1;
        }

        public void addAccount(string email, string pwd, string firstname, string lastname, string age, string address, string[] interests) {

            Account newUser = new Account {
                Email = email,
                Pwd = pwd,
                Firstname = firstname,
                Lastname = lastname,
                age = age,
                Address = address,
                Interests = interests
            };
        }
    } // ./ Class

}

// Dag: ___ Maand: ___ Jaar: ___

// Input - Username/Email AND Password
// Output - UID (User ID)
/*
namespace ConsoleApp1 { 
    public class Login {




        public int UID { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string[] Intrests { get; set; }


    }

    public class LoginArr
    {
        public Login[] Accounts { get; set; }
        
    }
}


*/
/*
public class Accounts
{
    public string Age;
    public string Email;
    public string Pwd;
    public string Firstname;
    public string Lastname;
    public string Address;
    public string[] Interests;

    public Account(string email)
    { // Constructor
        this.Email = email;
        this.Age = 0; // Note that if we left this line out, Age would have the same value as 0 is the default value for an int
    }
}
*/