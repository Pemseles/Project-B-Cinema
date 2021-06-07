using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
using ConsoleApp1;
namespace ConsoleApp1
{
    class Register {
        protected static Accounts NewUser = new Accounts();
        protected Restrictions check = new ConsoleApp1.Restrictions();
        
        private static int index = 0;
        private static string RegisterScreen(List<string> items)
        {
            Logo.Print();

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

            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key != ConsoleKey.DownArrow || ckey.Key != ConsoleKey.UpArrow || ckey.Key != ConsoleKey.Enter)
            {

            }
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index > 10)
                {
                    index = 11;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 0;
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }
            Console.Clear();
            return "";
        }

        public static void register()
        {
            // checks if the given number is logic
            static bool check(int EndValue, int beginvalue, int input)
            {
                
                Console.Clear();
                bool checker = false;
                if (input > beginvalue && input <= EndValue)
                {
                    checker = true;
                }
                
                return checker;
            }
            // checks if date is a number and realistic
            static string Date()
            {
                Console.Clear();
                int Days = 0;
                int Months = 0;
                int Years = 0;
                for (int index = 0; index < 3;)
                {
                    Console.Clear();
                    if (index == 0)
                    {
                        Console.WriteLine("(geef een dag tussen 1 en 31)");
                        Console.Write("Dag: ");
                        string Day = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(3, Day, 0) == false)
                        {
                            index = 0;
                        }
                        else
                        {
                            Days = int.Parse(Day);
                            bool day = check(31, 0, Days);
                            index++;
                            if (day == false)
                            {
                                index = 0;
                            }
                        }
                    }
                    if (index == 1)
                    {
                        Console.WriteLine("(geef een maand tussen 1 en 12)");
                        Console.Write("Maand: ");
                        string Month = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(3, Month, 0) == false)
                        {
                            index = 1;
                        }
                        else
                        {
                            Months = int.Parse(Month);
                            bool month = check(12, 0, Months);
                            index++;
                            if (month == false)
                            {
                                index = 1;
                            }
                        }
                    }
                    if (index == 2)
                    {
                        Console.WriteLine("(geef een jaar tussen 1900 en 2018)");
                        string Year = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(3, Year, 0) == false)
                        {
                            index = 2;

                        }
                        else
                        {
                            Years = int.Parse(Year);
                            bool year = check(2018, 1900, Years);
                            index++;
                            if (year == false)
                            {
                                index = 2;
                            }
                        }
                    }
                }
                Console.Clear();

                return $"{Days}/{Months}/{Years}";
            }
            // let's you enter a firstname or a lastname
            static string Name(int x)
            {
                string name = "";
                Console.Clear();
                    switch (x)
                    {
                        case 1:
                        Console.WriteLine("(geef een naam op die bestaat uit alleen maar letters)");
                        Console.Write("voer uw naam in: ");
                            name = Console.ReadLine();
                            if(ConsoleApp1.Restrictions.SW(5, name, 0) == true && name.Length > 1)
                            {
                                break;
                            }
                            name = Name(1);
                            break;
                        case 2:
                        Console.WriteLine("(geef een achternaam op die bestaat uit alleen maar letters)");
                        Console.Write("voer uw achternaam in: ");
                            name = Console.ReadLine();
                            if (ConsoleApp1.Restrictions.SW(5, name, 0) == true && name.Length > 1)
                            {
                                break;
                            }
                            name = Name(2);
                            break;
                    }               
                return name;
            }
            // let's you enter a email that contains a '@' and a '.'
            static string Email(bool clear=true)
            {
                if(clear) {Console.Clear();}
                    
                Console.WriteLine("(het e-mailadres moet een @ en een . bevatten)");
                Console.Write("voer uw e-mailadres in: ");
                string email = Console.ReadLine();
                for (int i = 0; i < email.Length; i++)
                {
                    if (email.Contains('@') && email.Contains('.') && email.Length > 4)
                    {
                        int ats = 0;
                        int dotssecondpart = 0;
                        foreach (char c in email)
                            if (c == '@') ats++;
                        foreach (char c in email.Split('@')[1])
                            if (c == '.') dotssecondpart++;
                        if (!NewUser.CheckUniqueEmail(email))
                        {
                            Console.Clear();
                            clear = false;
                            break;
                        } else { 
                            if (email.Split('@').Length == 2 && ats == 1 && email.Split('@')[0].Length > 0 && email.Split('@')[1].Length > 2 && dotssecondpart == 1)
                            {
                                string firstpart = email.Split('@')[0];
                                string secondpart = email.Split('@')[1].Split('.')[0];
                                string thirdpart = email.Split('@')[1].Split('.')[1];
                                if (firstpart.Length > 0 && secondpart.Length > 0 && thirdpart.Length > 0)
                                {
                                    Console.Clear();
                                    return email;
                                }
                            }
                        }
                    }
                }
                if(!(NewUser.CheckUniqueEmail(email))) {
                    Console.WriteLine("Deze Email is al in gebruik.");
                }
                Email(clear);
                return "";
            }
            static string Password()
            {
                Console.Clear();
                var password = "";
                var check = "";
                bool pw = true;
                string pw_strenght = "[Voer een wachtwoord in.]                     ";
                while (pw == true)
                {
                    // let's you put in a password and checks if it has an digit and a uppercase letter in it.

                    int digit = 0, letter = 0;
                    Console.Clear();
                    Console.WriteLine(pw_strenght);
                    Console.Write("Voer een wachtwoord in dat minimaal 1 hoofdletter en cijfer er in heeft zitten en minimaal 8 tekens bevat: ");
                    password = string.Empty;
                    ConsoleKey key;
                    do
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        key = keyInfo.Key;

                        if (key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            Console.Write("\b \b");
                            password = password[0..^1];
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            Console.Write("*");
                            password += keyInfo.KeyChar;
                        }
                    } while (key != ConsoleKey.Enter);
                    Console.Clear();
                    for (int c = 0; c < password.Length; c++)
                    {
                        // check uppercase letter
                        if (char.IsUpper(password, c))
                        {
                            letter = 1;
                            pw_strenght = "[Je wachtwoord mist een nummer.]               ";
                            Console.Write(pw_strenght);
                        }
                        // check digit 
                        if (char.IsDigit(password, c))
                        {
                            digit = 1;
                            pw_strenght = "[Je wachtwoord mist een hoofdletter.]  ";
                        }
                        if (digit == 0 && letter == 0)
                        {
                            pw_strenght = "[Je wachtwoord mist een hoofdletter en een cijfer.]  ";
                        }
                    }
                    // checks if the password is 8 or longer
                    if (password.Length < 8)
                    {
                        pw = true;
                        pw_strenght = "[Je wachtwoord is niet lang genoeg.]    ";

                    }
                    // check if you are sure you want to use this as your password 
                    if (password.Length >= 8 && (digit == 1 && letter == 1))
                    {
                        Console.Clear();
                        Console.Write("Voer het wachtwoord opnieuw in om het te bevestigen :");
                        check = string.Empty;
                        ConsoleKey key1;
                        do
                        {
                            var keyInfo = Console.ReadKey(intercept: true);
                            key1 = keyInfo.Key;

                            if (key1 == ConsoleKey.Backspace && password.Length > 0)
                            {
                                Console.Write("\b \b");
                                check = check[0..^1];
                            }
                            else if (!char.IsControl(keyInfo.KeyChar))
                            {
                                Console.Write("*");
                                check += keyInfo.KeyChar;
                            }
                        } while (key1 != ConsoleKey.Enter);
                        if (password == check)
                        {
                            pw = false;
                        }
                        else
                        {
                            pw = true;
                        }
                    }

                }
                return password;
            }
            //let's you enter your intrests
            static string[] Intrest()
            {
                string times = "";
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("voer een getal in");
                    Console.Write("hoeveel intresses heeft u: ");
                    times = Console.ReadLine();
                    if (ConsoleApp1.Restrictions.SW(3, times, 0) == true)
                    {
                        break;
                    }
                }
                int Times = int.Parse(times);
                string[] intrest = new string[Times];
                for (int i = 0; i < Times; i++)
                {                
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("je antwoord mag alleen maar letters bevatten");
                        Console.Write("voer een intresse in: ");
                        intrest[i] = Console.ReadLine();
                        if(ConsoleApp1.Restrictions.SW(5, intrest[i], 0) == true && intrest[i].Length > 1)
                        {
                            break;
                        }
                    }
                }
                return intrest;
            }
            // <add restrictions>!
            static string Address(int num)
            {
                Console.Clear();
                string address = "";
                switch (num) {
                    case 1:
                        // langste straatnaam is 55 letters in Nederland
                        Console.WriteLine("je antwoord mag alleen maar letters bevatten");
                        Console.Write("voer uw straat in: ");
                        address = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(5, address, 0) == true && address.Length < 56)
                        {
                            break;                           
                        }
                        address = Address(1);
                        break;
                        
                    case 2:
                        // hoogste huisnummer in Nederland is 5 getallen lang
                        Console.WriteLine("je antwoord mag alleen maar cijfers bevatten en niet langer dan 6 tekens zijn");
                        Console.Write("voer uw huisnummer in: ");
                        address = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(3, address, 0) == true && address.Length < 6)
                        {
                            break;
                        }
                        address = Address(2);
                        break;
                    case 3:
                        Console.WriteLine("Voer de eerste 4 cijfers van uw postcode in: ");
                        address = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(3, address, 0) == true && address.Length == 4)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voer de laatste 2 hoofdletters van uw postcode in: ");
                                Console.Write(address);
                                
                                string add = Console.ReadLine();
                                if(add.Length == 2 && ConsoleApp1.Restrictions.SW(5, add, 0) == true && ConsoleApp1.Restrictions.SW(2, add, 0) == true)
                                {
                                    address += add;
                                    break;
                                }
                            }       
                            break;
                        }
                        address = Address(3);
                        break;
                    case 4:
                        // langste woonplaatsnaam is 25 tekens lang
                        Console.WriteLine("je antwoord mag alleen maar letters bevatten");
                        Console.Write("voer uw woonplaats in: ");
                        address = Console.ReadLine();
                        if (ConsoleApp1.Restrictions.SW(5, address, 0) == true && address.Length < 26)
                        {
                            break;
                        }
                        address = Address(4);
                        break;
                }
                return address;      
            }
            bool registerbool = true;
            string Firstname = "";
            string Lastname = "";
            string age = "";
            string street = "";
            string housenum = "";
            string postalcode = "";
            string city = "";
            string email = "";
            string password = "";
            string[] intrest = new string[0];
            string error = "";
            string intreststring = "";
            string pass = "";

            while (registerbool == true)
            {
                Console.Clear();

                List<string> LoginScreens = new List<string>() {
                "[     Voornaam     ]" + Firstname,
                "[    Achternaam    ]" + Lastname,
                "[   Geboortedatum  ]" + age,
                "[    Straatnaam    ]" + street,
                "[    Huisnummer    ]" + housenum,
                "[     Postcode     ]" + postalcode,
                "[    Woonplaats    ]" + city,
                "[      E-mail      ]" + email,
                "[    Wachtwoord    ]" + pass,
                "[    Interessen    ]" + intreststring,
                "[ Account aanmaken ]" + error,
                "[     Annuleren    ]"
                 };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = RegisterScreen(LoginScreens);
                if (selectedMenuItem == "[     Voornaam     ]" + Firstname)
                {
                    Firstname = Name(1);
                }
                if (selectedMenuItem == "[    Achternaam    ]" + Lastname)
                {
                    Lastname = Name(2);
                }
                if (selectedMenuItem == "[   Geboortedatum  ]" + age)
                {
                    age = Date();
                }
                if (selectedMenuItem == "[    Straatnaam    ]" + street)
                {
                    street = Address(1);
                }
                if (selectedMenuItem == "[    Huisnummer    ]" + housenum)
                {
                    housenum = Address(2);
                }
                if (selectedMenuItem == "[     Postcode     ]" + postalcode)
                {
                    postalcode = Address(3);
                }
                if (selectedMenuItem == "[    Woonplaats    ]" + city)
                {
                    city = Address(4);
                }
                if (selectedMenuItem == "[      E-mail      ]" + email)
                {
                    email = Email();
                }
                if (selectedMenuItem == "[    Wachtwoord    ]" + pass)
                {
                    password = Password();
                    for (int i = 0; i < password.Length; i++)
                    {
                        pass += "*";
                    }
                }
                if (selectedMenuItem == "[    Interessen    ]" + intreststring)
                {
                    intrest = Intrest();
                    intreststring = "";
                    for (int i = 0; i < intrest.Length; i++)
                    {
                        intreststring += intrest[i] + " ";
                    }
                }
                if (selectedMenuItem == "[ Account aanmaken ]" + error)
                {
                    if (Firstname != "" && Lastname != "" && age != "" && street != "" && housenum != "" && postalcode != "" && city != "" && email != "" && password != "" && intreststring != "")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        error = "U heeft niet alle velden ingevuld";
                    }
                }
                if(selectedMenuItem == "[     Annuleren    ]")
                {                   
                    Console.Clear();
                    registerbool = false;
                }
                }
            string address = street + housenum + postalcode + city;
            NewUser.AddAccount(email, password, Firstname, Lastname, age, address, intrest);
        }
    }
}
