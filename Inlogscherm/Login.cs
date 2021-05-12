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
        public static void loginFunc()
        {
            
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
                login = true;

                Console.Clear();
                ConsoleApp1.Accounts userLogin = new Accounts();
                if(userLogin.Login(email, pass) == -1)
                {
                    Console.WriteLine("Email or Wachtwoord is onjuist\nProbeer het nog een keer");
                    loginFunc();

                }
            }
            // aanpassing maken naar false -- t gaat fout tot t goed gaat
        }
    }
}