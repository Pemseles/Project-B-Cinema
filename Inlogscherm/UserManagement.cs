using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    /* --------------------- User Management --------------------- */
    public class ShowUserAccounts : MenuController
    {  // Inherits from Abstract Class
        protected static Accounts CurrentUser = new Accounts();
        public static void mainMenu()
        {
            // Start the Menu
            bool mainmenubool = true;
            while (mainmenubool == true)
            {
                Logo.Print();
                List<string> Mainscreen = new List<string>() {
                "[   Inactive Accounts weergeven  ]" , // 0 
                "[    Active Accounts weergeven   ]" , // 1 
                "[           Terugkeren           ]"   // 2
                };
                // kijkt bij welke index de user zich bevind
                string selectedMenuItem = MainScreen(Mainscreen);

                if (selectedMenuItem == Mainscreen[0]) // Show Suspended Accounts
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    UserManagement usermanagement = new UserManagement();
                    usermanagement.ShowInactiveAccounts();
                    // ./ Contents
                    back();
                }
                else if (selectedMenuItem == Mainscreen[1]) // Show Active Accounts
                {
                    ResetIndex();
                    Console.Clear();
                    // Contents
                    UserManagement usermanagement = new UserManagement();
                    usermanagement.showActiveAccounts();
                    // ./ Contents
                    back();
                }
                // Mainscreen.Count -1 to always get the last element of the list, as Logout is always last. 
                else if (selectedMenuItem == Mainscreen[Mainscreen.Count - 1]) // Logout
                {
                    ResetIndex();
                    Console.Clear();
                    AdminMenu.Mainmenu();
                }
                Console.Clear();
            }
        }
    }

    public class UserManagement
    {
        protected Admin admin = new Admin(Program.UID);
        protected Accounts accountsObvj = new Accounts();
        public void ShowInactiveAccounts()
        {

            List<Account> accounts = Admin.GetAllActiveAccounts(false);

            accounts.Reverse();
            int place = 2;
            int revs = 0;

            // Opmaak
            string accountMargin = "             ";
            string marginLeft = "  ";
            string marginRight = "                              ";

            //ReviewCreation.FilterReviews();
            int total_revs = accounts.Count;


            if (total_revs > 0)
            {
                while (true)
                {
                    if (revs < 0) { revs = 0; }
                    string actualName;
                    int authorID = accounts[revs].UID;
     
                    Tuple<string, string> temp = Accounts.GetFullname(authorID);
                    actualName = $"{temp.Item1} {temp.Item2}";
                    string allInterests = "";
                    foreach (string interest in accounts[revs].Interests)
                    {
                        allInterests += interest + ", ";
                    }

                    if (place == 1 && accounts[revs].UID == -1) { place = 0; }
                    Logo.Print();
                    Console.WriteLine(accountMargin +"E-mail:         " + accounts[revs].Email);
                    Console.WriteLine(accountMargin +"Volledige naam: " + actualName);
                    Console.WriteLine(accountMargin +"Geboortedatum:  " + accounts[revs].Age);
                    Console.WriteLine(accountMargin +"Adres:          " + accounts[revs].Address);
                    Console.WriteLine(accountMargin +"Interesses:     " + allInterests);
                    Console.Write(" ");

                    if (place == 0) // Activate
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[  Account Activeren  ]");
                        Console.ResetColor();
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 1)
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account          
                        Console.Write("[  Account Activeren  ]");
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("< Vorige >");
                        Console.ResetColor();
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 2)
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account          
                        Console.Write("[  Account Activeren  ]");
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                        Console.Write("< Volgende >");
                        Console.ResetColor();

                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.LeftArrow || ckey.Key != ConsoleKey.RightArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.RightArrow)
                    {
                        place = place != 2 ? place + 1 : place = 2;
                        if (place == 1 && accounts[revs].UID == -1) { place += 1; }
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {
                        place = place != 0 ? place - 1 : place = 0;
                        if (place == 1 && accounts[revs].UID == -1) { place -= 1; }
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        if (place == 0)
                        {
                            // Suspend the account
                            admin.ActivateAccount(accounts[revs].UID);
                            // And remove from local list
                            accounts.RemoveAt(revs--);
                            total_revs--;

                        }
                        if (place == 1)
                        {
                            if (revs > 0)
                                revs -= 1;
                        }
                        if (place == 2)
                        {
                            if (revs < total_revs - 1)
                                revs += 1;
                        }
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                    if (total_revs == 0)
                    {
                        break;
                    }
                } // ./ While Loop
                if (accounts.Count <= 0)
                {
                    Console.WriteLine("Er zijn (nog) geen Inactieve Accounts");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("[     Terug     ]");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.ResetColor();
                        Console.Clear();
                        AdminMenu.Mainmenu();
                    }
                }
            }
        }
        public void showActiveAccounts()
        {
            List<Account> accounts = Admin.GetAllActiveAccounts(true);
            accounts.Reverse();
            int place = 2;
            int revs = 0;

            // Opmaak
            string accountMargin = "             ";
            string marginLeft = "  ";
            string marginRight = "                        ";

            //ReviewCreation.FilterReviews();
            int total_revs = accounts.Count;


            if (total_revs > 0)
            {
                while (true)
                {
                    if (revs < 0) { revs = 0; }
                    string actualName;
                    int authorID = accounts[revs].UID;

                    Tuple<string, string> temp = Accounts.GetFullname(authorID);
                    actualName = $"{temp.Item1} {temp.Item2}";
                    string allInterests = "";
                    foreach (string interest in accounts[revs].Interests)
                    {
                        allInterests += interest + ", ";
                    }

                    if (place == 1 && accounts[revs].UID == -1) { place = 0; }
                    Logo.Print();
                    Console.WriteLine(place);
                    Console.WriteLine(accountMargin + "E-mail:         " + accounts[revs].Email);
                    Console.WriteLine(accountMargin + "Volledige naam: " + actualName);
                    Console.WriteLine(accountMargin + "Geboortedatum:  " + accounts[revs].Age);
                    Console.WriteLine(accountMargin + "Adres:          " + accounts[revs].Address);
                    Console.WriteLine(accountMargin + "Interesses:     " + allInterests);
                    Console.Write(" ");

                    if (place == 0) // Activate
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[  Account Deactiveren  ]");
                        Console.ResetColor();
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 1)
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account          
                        Console.Write("[  Account Deactiveren  ]");
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("< Vorige >");
                        Console.ResetColor();
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 2)
                    {
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        // Account          
                        Console.Write("[  Account Deactiveren  ]");
                        Console.Write(" ");
                        // ./ Account
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                        Console.Write("< Volgende >");
                        Console.ResetColor();

                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key != ConsoleKey.LeftArrow || ckey.Key != ConsoleKey.RightArrow || ckey.Key != ConsoleKey.Enter || ckey.Key != ConsoleKey.Backspace)
                    {

                    }
                    if (ckey.Key == ConsoleKey.RightArrow)
                    {
                        place = place != 2 ? place + 1 : place = 2;
                        if (place == 1 && accounts[revs].UID == -1) { place += 1; }
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {
                        place = place != 0 ? place - 1 : place = 0;
                        if (place == 1 && accounts[revs].UID == -1) { place -= 1; }
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        if (place == 0)
                        {
                            // Suspend the account
                            admin.SuspendAccount(accounts[revs].UID);
                            // And remove from local list
                            accounts.RemoveAt(revs--);
                            total_revs--;

                        }
                        if (place == 1)
                        {
                            if (revs > 0)
                                revs -= 1;
                        }
                        if (place == 2)
                        {
                            if (revs < total_revs - 1)
                                revs += 1;
                        }
                    }
                    else if (ckey.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                    if (total_revs == 0)
                    {
                        break;
                    }
                } // ./ While Loop
                if (accounts.Count <= 0)
                {
                    Console.WriteLine("Er zijn (nog) geen Actieve Accounts");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("[     Terug     ]");
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.ResetColor();
                        Console.Clear();
                        AdminMenu.Mainmenu();
                    }
                }
            }
        }
    }
}