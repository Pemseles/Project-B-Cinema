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
    public class Admin : Utility // Inherits from Abstract Class
    {
        private int AdminID;
        private string AccessDenied = "U hebt onvoldoende rechten om deze actie uit te voeren.";
        public Accounts TheUser = new Accounts();
        public Admin(int UID) { this.AdminID = UID; }

        public void AddNewFilm(string filmName, string director, string[] genres, int ageRating)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var filmList = JsonConvert.DeserializeObject<List<Film>>(jsonData) ?? new List<Film>();

            // Add a new Film
            filmList.Add(new Film()
            {
                ID = GenerateID(filmPath),
                Name = filmName,
                Director = director,
                Genres = genres,
                AgeRating = ageRating
            });

            // Update json data string
            jsonData = JsonConvert.SerializeObject(filmList, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(filmPath, jsonData);
        }

        public void AddFilmInstance(int movieID, int theaterhallID, string startDateTime, string endDateTime, string type, decimal price)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var filmInstances = JsonConvert.DeserializeObject<List<Filminstance>>(jsonData) ?? new List<Filminstance>();

            // Add a new Film
            filmInstances.Add(new Filminstance()
            {
                ID = GenerateID(filmInstancesPath),
                MovieID = movieID,
                TheaterhallID = theaterhallID,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Type = type,
                Price = price,
                Active = true
            });
            // Update json data string
            jsonData = JsonConvert.SerializeObject(filmInstances, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(filmPath, jsonData);
        }

        public void RemoveFilmInstance(int id)
        {
            /// Takes Int ID as Parameter to locate & Remove Entry with Matching ID
            var FilmInstances = retrieveJson(filmInstancesPath);
            int index = 0;
            foreach (Filminstance filmInstance in FilmInstances)
            {
                if (id == filmInstance.ID)
                    break;
                index++;
            }
            // Remove entry and update file
            FilmInstances.RemoveAt(index);
            InsertJson(FilmInstances, filmInstancesPath);
        }

        public void RemoveFilm(int id)
        {
            /// Takes Int ID as Parameter to locate & Remove Parent + Child Entry with Matching ID.
            var FilmList = retrieveJson(filmPath);
            var FilmInstances = retrieveJson(filmInstancesPath);

            int index = 0;
            // Locate and Remove all Child Entries
            foreach (Filminstance filmInstance in FilmList)
            {
                if (id == filmInstance.MovieID)
                {
                    FilmInstances.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            index = 0;
            // Then Locate Parent
            foreach (Film film in FilmList)
            {
                if (id == film.ID) { break; }
                index++;
            }
            // Remove entry and update files
            FilmList.RemoveAt(index);
            InsertJson(FilmList, filmPath);
            InsertJson(FilmInstances, filmInstancesPath);
        }

        /// <summary>
        /// Adds a new theatherhall
        /// </summary>
        /// <param name="hallName">Name of the hall eg. Zaal 1</param>
        /// <param name="numberOfSeats">The number of seats this Hall has, even number adviced</param>
        /// <param name="vipSeatCoords">The Coordinates of the VIP Seats, a list of ints</param>
        /// <param name="active">a bool wether this hall is active (recognised by the system. Default=true</param>
        public void AddTheatherhall(string hallName, int numberOfSeats, List<int> vipSeatCoords, bool active = true)
        {
            var jsonData = System.IO.File.ReadAllText(filmPath);
            var theatherhallList = JsonConvert.DeserializeObject<List<Theaterhall>>(jsonData) ?? new List<Theaterhall>();

            // Add a new Film
            theatherhallList.Add(new Theaterhall()
            {
                ID = GenerateID(theatherhallPath),
                Name = hallName,
                NumberOfSeats = numberOfSeats,
                VipSeatCoords = vipSeatCoords,
                Active = active
            });
            // Update json data string
            jsonData = JsonConvert.SerializeObject(theatherhallList, Formatting.Indented);
            // Serialize JSON to a string and then write string to a file
            System.IO.File.WriteAllText(theatherhallPath, jsonData);
        }

        /// <summary>
        /// Suspends a users account
        /// Eg. When the user disobeys the TOS
        /// </summary>
        /// <param name="UID">The Users ID (int)</param>
        public void SuspendAccount(int UID)
        { /// Suspends the Account where ID matches with given int Parameter
            if (TheUser.GetLevel(AdminID) >= 3)
            {

                var jsonData = System.IO.File.ReadAllText(accountPath);
                var AccountList = JsonConvert.DeserializeObject<List<Account>>(jsonData);

                foreach (Account Account in AccountList)
                {
                    if (UID == Account.UID)
                    {
                        Account.Active = false;
                        break;
                    }
                }
            } else  {
                Console.WriteLine(AccessDenied);
            }
        }

        /// <summary>
        /// Activate Account Method
        /// Re-Actviates the Account where ID matches with given int Parameter
        /// </summary>
        /// <param name="UID">(int) The User's Account ID that needs to reactivated</param>
        public void ActivateAccount(int UID)
        { /// Re-Actviates the Account where ID matches with given int Parameter
            if (TheUser.GetLevel(AdminID) == 3)
            {

                List<Object> AccountList = retrieveJson(accountPath);
                foreach (Account account in AccountList)
                {
                    if (UID == account.UID)
                    {
                        account.Active = true;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(AccessDenied);
            }
        }

        /// <summary>
        /// Lists all the Suspended Accounts.
        /// </summary>
        /// <returns>A List with all the Suspended Accounts</returns>
        public List<Account> GetAllSuspendedAccounts()
        { /// Lists all Suspended Adcounts
            List<Account> AccountList = Accounts.RetrieveAccountData();
            List<Account> SuspendedAccounts = new List<Account>();
            foreach (Account account in AccountList)
            {
                if (account.Active == false)
                {
                    SuspendedAccounts.Add(account);
                }
            }
            return SuspendedAccounts;
        }

        /// <summary>
        /// Lists all Accounts based on active Status.
        /// </summary>
        /// <returns>A List with all the Active Accounts</returns>
        public static List<Account> GetAllActiveAccounts(bool activeStatus=true)
        { /// Lists all Suspended Adcounts
            List<Account> AccountList = Accounts.RetrieveAccountData();
            List<Account> ActiveAccounts = new List<Account>();
            foreach (Account account in AccountList)
            {
                if (account.Active == activeStatus)
                {
                    ActiveAccounts.Add(account);
                }
            }
            Console.WriteLine("oqw");
            return ActiveAccounts;
        }
    }

    /* --------------------- ADMIN MENUS --------------------- */

    public class ShowReviewsAdmin
    {
        protected Admin admin = new Admin(Program.UID);
        protected Accounts accountsObvj = new Accounts();
        public void review_list()
        {
            List<Review> reviews = ReviewCreation.GetActiveReviews(); ;
            reviews.Reverse();
            int place = 3;
            int revs = 0;

            // Opmaak
            string marginLeft = "  ";
            string marginRight = "                        ";

            int total_revs = reviews.Count;


            if (total_revs > 0)
            {
                while (true)
                {
                    if(revs < 0) { revs = 0; }
                    string actualName;
                    int authorID = reviews[revs].UID;

                    if (authorID == -1) { 
                        actualName = "Gast";
                    } else {
                        Tuple<string, string> temp = Accounts.GetFullname(authorID);
                        actualName = $"{temp.Item1} {temp.Item2}";
                    }

                    if (place == 1 && reviews[revs].UID == -1) { place = 0; }
                    Logo.Print();
                    Console.Write("                                  Gebruikersnaam: " + reviews[revs].Author + $" ({actualName})" + "         "); Console.WriteLine("waardering: " + reviews[revs].Stars);
                    Console.WriteLine();
                    Console.WriteLine("                                       [ " + reviews[revs].Title + " ]");
                    Console.WriteLine();
                    Console.Write("                                  "); Console.Write(reviews[revs].Description);
                    Console.WriteLine();

                 
                    if (place == 0) // Delete
                    {
                        // Admin 
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[  Recensie Verwijderen  ]");
                        Console.ResetColor();
                        if(reviews[revs].UID == -1) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                        Console.Write("[  Account Deactiveren  ]");
                        if (reviews[revs].UID == -1)
                        {
                            Console.ResetColor();
                        }
                        Console.Write(" ");
                        // ./ Admin
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 1) // Suspend
                    {
                        // Admin 
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        Console.Write("[  Recensie Verwijderen  ]");
                        if (reviews[revs].UID == -1) {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        } else {
                            Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write("[  Account Deactiveren  ]");
                        Console.ResetColor();
                        Console.Write(" ");
                        // ./ Admin
                        Console.Write(marginRight);
                        Console.Write("< Vorige >");
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 2) // Deactivate
                    {
                        // Admin 
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        Console.Write("[  Recensie Verwijderen  ]");
                        if (reviews[revs].UID == -1) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                        Console.Write("[  Account Deactiveren  ]");
                        if (reviews[revs].UID == -1)
                        {
                            Console.ResetColor();
                        }
                        Console.Write(" ");
                        // ./ Admin
                        Console.Write(marginRight);
                        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("< Vorige >");
                        Console.ResetColor();
                        Console.Write(" ");
                        Console.Write($" < {revs + 1} / {total_revs} > ");
                        Console.Write(" ");
                        Console.Write("< Volgende >");

                    }
                    if (place == 3)
                    {
                        // Admin 
                        Console.WriteLine();
                        Console.Write(marginLeft);
                        Console.Write("[  Recensie Verwijderen  ]");
                        if (reviews[revs].UID == -1) {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        Console.Write("[  Account Deactiveren  ]");
                        if (reviews[revs].UID == -1)
                        {
                            Console.ResetColor();
                        }
                        Console.Write(" ");
                        // ./ Admin
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
                        place = place != 3 ? place + 1 : place = 3;
                        if(place == 1 && reviews[revs].UID == -1){ place += 1;}
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow)
                    {
                        place = place != 0 ? place- 1 : place = 0;
                        if (place == 1 && reviews[revs].UID == -1) { place -= 1; }
                    }
                    else if (ckey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        if (place == 0)
                        {
                            ReviewCreation.DeleteReview(reviews[revs].ID);
                            reviews.RemoveAt(revs--);
                            total_revs--;

                        }
                        if (place == 1)
                        {
                            if (reviews[revs].UID >= 0) {
                                // Suspend the account
                                admin.SuspendAccount(reviews[revs].UID);
                                // And remove from local list
                                reviews.RemoveAt(revs--);
                                total_revs--;
                            }
                        }
                        if (place == 2)
                        {
                            if (revs > 0)
                                revs -= 1;
                        }
                        if (place == 3)
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
                if(reviews.Count <= 0)
                {
                    Console.WriteLine(marginLeft + "Er zijn (nog) geen Resensies");
                    Console.Write(marginLeft);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("[     Terug     ]");
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