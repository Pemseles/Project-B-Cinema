using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;

namespace ConsoleApp1
{
    class Register
    {
        static void Main(string[] args)
        {
            // checks if the given number is logic
            static bool check(int EndValue, int beginvalue, int input)
            {
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
                int Days = 0;
                int Months = 0;
                int Years = 0;
                for (int index = 0; index < 3;)
                {
                    Console.Clear();
                    if (index == 0)
                    {
                        Console.Write("Dag: ");
                        string Day = Console.ReadLine();
                        Days = int.Parse(Day);
                        bool day = check(31, 0, Days);
                        index++;
                        if (day == false)
                        {
                            index = 0;
                        }
                    }
                    if (index == 1)
                    {
                        Console.Write("Maand: ");
                        string Month = Console.ReadLine();
                        Months = int.Parse(Month);
                        bool month = check(12, 0, Months);
                        index++;
                        if (month == false)
                        {
                            index = 1;
                        }
                    }
                    if (index == 2)
                    {
                        Console.Write("Jaar: ");
                        string Year = Console.ReadLine();
                        Years = int.Parse(Year);
                        bool year = check(2021, 1900, Years);
                        index++;
                        if (year == false)
                        {
                            index = 2;
                        }
                    }
                }
                Console.Clear();
                return $"{Days}/{Months}/{Years}";
            }
            // let's you enter a firstname or a lastname
            static string Name(int x)
            {
                if (x == 0)
                {
                    Console.Write("voer uw naam in: ");
                }
                else
                {
                    Console.Write("voer uw achternaam in: ");
                }
                string name = Console.ReadLine();
                for (int i = 0; i < name.Length; i++)
                {
                    if (char.IsDigit(name, i))
                    {
                        Console.Clear();
                        Name(x);
                    }
                }
                return name;
            }
            // let's you enter a email that contains a '@' and a '.'
            static string Email()
            {
                Console.Write("voer uw e-mailadres in: ");
                string email = Console.ReadLine();
                for (int i = 0; i < email.Length; i++)
                {
                    if (email.Contains('@') && email.Contains('.') && email.Length > 10)
                    {
                        Console.Clear();
                        return email;
                    }
                }
                Email();
                return "";
            }
            static string Password()
            {
                var password = "";
                bool pw = true;
                string pw_strenght = "[Voer een wachtwoord in.]                     ";
                while (pw == true)
                {
                    // let's you put in a password and checks if it has an digit and a uppercase letter in it.

                    int digit = 0, letter = 0;
                    Console.Clear();
                    Console.WriteLine(pw_strenght);
                    Console.Write("Voer een wachtwoord in dat minimaal 1 hoofdletter en 1 cijfer er in heeft zitten: ");
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
                        string check = Console.ReadLine();
                        if (check == password)
                        {
                            //checks the pressed key
                            Console.Clear();
                            Console.WriteLine("Om uw account te creëren, druk <ENTER>. om uw gegevens opnieuw in te vullen, druk <BACKSPACE>.");
                            ConsoleKeyInfo pressedkey = ReadKey();

                            if (pressedkey.Key == ConsoleKey.Enter)
                            {
                                pw = false;
                                Console.Clear();
                                break;

                            }
                            if (pressedkey.Key == ConsoleKey.Backspace)
                            {
                                Console.Clear();
                                pw = true;
                            }
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
                Console.Write("hoeveel intresses heeft u: ");
                string times = Console.ReadLine();
                int Times = int.Parse(times);
                string[] intrest = new string[Times];
                for (int i = 0; i < Times; i++)
                {
                    Console.Write("voer een intresse in: ");
                    intrest[i] = Console.ReadLine();
                }
                return intrest;
            }
            // <add restrictions>!
            static string Address()
            {
                Console.Write("voer uw straat in: ");
                string street = Console.ReadLine();
                Console.Write("voer uw huisnummer in: ");
                string housenum = Console.ReadLine();
                Console.Write("voer uw postcode in: ");
                string zipcode = Console.ReadLine();
                Console.Write("voer uw woonplaats in: ");
                string city = Console.ReadLine();

                return $"{street } {housenum } {zipcode } {city }";

            }
            string address = Address();
            string[] intrest = Intrest();
            string Firstname = Name(0);
            string Lastname = Name(1);
            string date = Date();
            string email = Email();
            string password = Password();
            ConsoleApp1.Accounts NewUser = new Accounts();
            NewUser.AddAccount(email, password, Firstname, Lastname, date, address, intrest);
        }
    }
}
