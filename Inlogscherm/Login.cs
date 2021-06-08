using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;

namespace ConsoleApp1
{
    public class Login
    {
        public bool fail = false;
        public static int loginFunc()
        {
            Accounts userLogin = new Accounts();

            bool login = false;
            while (login == false)
            {
                //Console.Clear();
                var pass = string.Empty;
                ConsoleKey key;
                Console.Write("E-mail: ");
                string email = Console.ReadLine();

                Console.Write("Wachtwoord: "); do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                string loginemail = Console.ReadLine();
               

                Console.Clear();
                if(pass.Length == 0 || email.Length == 0)
                {
                    Console.WriteLine("Voer alle velden in.");
                    return loginFunc();
                }
                if(userLogin.Login(email, pass) == -1)
                {
                    Console.WriteLine("E-mail of Wachtwoord is onjuist\nProbeer het nog een keer");
                    return loginFunc();

                }
                login = true;
                return userLogin.Login(email, pass);
            }
            return -1;
            // aanpassing maken naar false -- t gaat fout tot t goed gaat
        }
    }
}