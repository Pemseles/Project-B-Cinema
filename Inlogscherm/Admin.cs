using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Linq;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class Admin : Utility // Inherits from Abstract Class
    {
        private int AdminID;
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
            List<Object> AccountList = retrieveJson(accountPath);
            foreach (Account Account in AccountList)
            {
                if (UID == Account.UID)
                {
                    Account.Active = false;
                    break;
                }
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
    }
}