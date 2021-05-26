using System;
using System.Collections.Generic;
using static ConsoleApp1.MainMenu;
namespace ConsoleApp1
{
    class MyAccount
    {
        public int UID;
        public Accounts CurrentUser;
        public List<string> AccountMenu;
        public int index;
        public MyAccount(int uid)
        {
            this.UID = uid;
            this.CurrentUser = new Accounts();
            this.AccountMenu = new List<string>() {
                "[   Email Wijzigen    ]" ,
                "[ Wachtwoord Wijzigen ]",
                "[  Account Upgraden   ]",
                "[       Terug         ]"
                 };
        }

        public void resetIndex()
        {
            index = 0;
        }

        static string Email()
        {
            Console.Clear();
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

        public void OpenMenu()
        {
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                string selectedMenuItem = MainMenu.MainScreen(AccountMenu);

                if (selectedMenuItem == AccountMenu[0]) // Change Email
                {
                    MainMenu.ResetIndex();
                    Console.Clear();
                    Console.Write("Weet je zeker dat je, je email wilt wijzigen?\nDruk op ENTER om verder te gaan.\nBACKSPACE om dit te annuleren.");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        OpenMenu();
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        string email = Email();
                        CurrentUser.UpdateEmail(this.UID, email);
                    }
                    OpenMenu();
                }
                else if (selectedMenuItem == AccountMenu[1])  // Change Password
                {
                    Console.Clear();
                    Console.Write("Weet je zeker dat je, je wachtwoord wilt wijzigen?\nDruk op ENTER om verder te gaan.\nBACKSPACE om dit te annuleren.");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        OpenMenu();
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("Voer opnieuw je gegevens in om je indentiteit te bewijzen.");
                        if (ConsoleApp1.Login.loginFunc() == this.UID)
                        {
                            Console.WriteLine("Nieuw Wachtwoord:\n");
                            CurrentUser.UpdatePwd(this.UID, Password());
                        } 
                        OpenMenu();
                    }
                }
                else if (selectedMenuItem == AccountMenu[2]) // Upgrade to VIP
                {
                    Console.Clear();
                    Console.Write("Upgrade nu naar VIP!\nDruk op ENTER om verder te gaan.\nBACKSPACE om dit te annuleren.");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        OpenMenu();
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        if (CurrentUser.CheckValidUprade(UID))
                        {
                            Console.Clear();
                            // :: INSERT PAYWALL HERE //////////////

                            Console.WriteLine("<< Paywall >>");

                            // :: ///////////////////////////////////////////
                        } else {
                            Console.WriteLine("Je bent al VIP.");
                        }

                        CurrentUser.UpgradeToVip(UID);
                    }

                    MainMenu.back();

                }
                else if (selectedMenuItem == AccountMenu[3])
                {
                    Console.Clear();
                    MainMenu.Mainmenu();
                }

            }

        }
    }
}