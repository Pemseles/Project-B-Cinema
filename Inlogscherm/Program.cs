using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

// using consoleapp1.mapnaam.filenaam;


namespace Console_Menu
{
    class Program
    {
        private static int index = 0;

        private static void Main(string[] args)
        {


            List<string> LoginScreen = new List<string>() {
                "[     Inloggen    ]",
                "[    Registreren   ]",
                "[  Verder als gast ]"
            };
             List<string> movieList = new List<string>() {
                "Titanic                                            - James Cameron               09:00-10:30          zaal 1       2D",
                "Avatar                                             - James Cameron               09:15-10:45          zaal 2       4D",
                "Ready Player One                                   - Steven Spielberg            10.25-11:55          zaal 3       3D",
                "Pulp Fiction                                       - Quentin Tarantino           11:20-12:50          zaal 4       DOLBY",
                "Interstellar                                       - Christopher Nolan           12:50-14:20          zaal 5       IMAX",
                "the lord of the rings the return of the king       - Peter Jackson               13:45-15:15          zaal 2       3D",
                "The Notebook                                       - Nick Cassavetes             15:40-17:10          zaal 1       IMAX",
                "Joker                                              - Todd Phillips               16:00-17:30          zaal 3       4D",
                "The Wolf of Wallstreet                             - Martin Scorsese             17:15-18:45          zaal 5       3D"
            };

            Console.CursorVisible = false;
            bool LoginStartScreen = true;
            while (LoginStartScreen)
            {
                string selectedMenuItem = MainScreen(LoginScreen);
                // Login
                if (selectedMenuItem == "[     Inloggen    ]")
                {
                    bool login = true;
                    while (login == true) {
                        Console.Clear();
                        var pass = string.Empty;
                        ConsoleKey key;
                        Console.Write("E-mail: "); Console.ReadLine();
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

                        login = false;
                        Console.Clear();
                        LoginStartScreen = false;
                        for (int i = 0; i < movieList.Count; i++)
                        {
                            Console.WriteLine(movieList[i]);
                            Console.WriteLine();
                        }
                        ConsoleKeyInfo ckey = Console.ReadKey();
                        if (ckey.Key == ConsoleKey.Enter)
                        {
                            Environment.Exit(0);
                        }


                    }

                }
                else if (selectedMenuItem == "[  Verder als gast ]")
                {

                    Console.Clear();
                    LoginStartScreen = false;
                    for (int i = 0; i < movieList.Count; i++)
                    {
                        Console.WriteLine(movieList[i]);
                        Console.WriteLine();
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                    
                }

                // Register
                else if (selectedMenuItem == "[    Registreren   ]")
                {
                    Console.Clear();
                    bool name = true;
                    while (name == true)
                    {
                        Console.Write("E-mail:"); string Username = Console.ReadLine();
                        if (Username.Length >= 8)
                        {
                            name = false;
                        }
                    }
                    bool pw = true;
                    string pw_strenght = "[Voer een wachtwoord in.]                     ";
                    while (pw == true)
                    {
                        // let's you put in a password and checks if it has an digit and a uppercase letter in it.

                        int digit = 0, letter = 0;
                        Console.Clear();
                        Console.WriteLine(pw_strenght);
                        Console.Write("Voer een wachtwoord in dat minimaal 1 hoofdletter en 1 cijfer er in heeft zitten: ");
                        var password = string.Empty;
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
                            if(check == password) {
                            //checks the pressed key
                            Console.Clear();
                             Console.WriteLine("Om uw account te creëren, druk <ENTER>. om uw gegevens opnieuw in te vullen, druk <BACKSPACE>.");
                             ConsoleKeyInfo pressedkey = ReadKey();
                            
                            if (pressedkey.Key == ConsoleKey.Enter)
                            {
                                pw = false;
                                Console.Clear();
                                LoginStartScreen = false;
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

                    for (int i = 0; i < movieList.Count; i++)
                    {
                        Console.WriteLine(movieList[i]);
                        Console.WriteLine();
                    }
                    ConsoleKeyInfo ckey = Console.ReadKey();
                    if (ckey.Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        private static string MainScreen(List<string> items)
        {
            
            Console.Write("                         ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝");
            Console.ResetColor();
            Console.Write("                        ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║     ███████╗");
            Console.ResetColor();
            Console.Write("                         ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            /*string logo = @"        
                         ██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗
                        ██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝
                        ██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗  
                        ██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝  
                        ╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║     ███████╗
                         ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝

           
";
             */

            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.Write("                                                        ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    
                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.Write("                                                        ");
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == 2)
                {
                    index = 0;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = 2;
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
    }
}