using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
using System.IO;



namespace ConsoleApp1 {
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class Account {
        public string username;
        public int Age;

        public Account(string name) // Constructor
        {
            this.username = name;
            this.Age = 0; // Note that if we left this line out, Age would have the same value as 0 is the default value for an int
        }

        public void createNewAccount() {
            var test = 0;
        }

        public void editAccount() {
            this.Age = this.Age + 1;
        }

        public void loadJson() {
            string accountJSONPath = Path.GetFullPath(@"Accounts.json");
            Console.WriteLine(accountJSONPath);
            string jsonStringAccountList = File.ReadAllText(accountJSONPath);
            /*
            ConsoleApp1.accArr accountList = new ConsoleApp1.accArr();
            accountList = JsonSerializer.Deserialize<ConsoleApp1.accArr>(jsonStringAccountList);*/
        }
    }


// Input - Username/Email AND Password
// Output - UID (User ID)

    public class Login {

        public string[] Accounts { get; set; }



        public int loginUID { get; set; }
        public int loginLevel { get; set; }
        public string loginEmail { get; set; }
        public string loginPwd { get; set; }
        public string loginFirstname { get; set; }
        public string loginLastname { get; set; }
        public int loginAge { get; set; }
        public string loginAddress { get; set; }
        public string[] loginintrests { get; set; }


    }

    public class LoginArr
    {
        public string[] Accounts { get; set; }
    }
}

