using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using static System.Console;


namespace ConsoleApp1
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class Film
    {
        // deze class heeft filmnaam, director, array met genres en agerating, die je kan omzetten tot JSON-string en van JSON tot object
        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string[] Genres { get; set; }
        public int AgeRating { get; set; }
    }
    
    public class FilmArr
    {
        // deze class heeft alle objects van de Film class
        public Film[] FilmArray { get; set; }
    }

    class SearchClass 
    {
        public string InputSearch;

        public SearchClass(string userInput)
        {
            this.InputSearch = userInput;
        }

        public List<Film> FilmSearch(FilmArr filmList)
        {
            List<Film> returnList = new List<Film>();
            // om hier iets aan toe te voegen returnList.Add("wat er aan de ijst toegeveogd moet worden")
            int inputAge = -513892;
            string[] numbersInputArray = { "0" , "6" , "9" , "12" , "14" , "16" , "18"};
            // string array met mogelijke kwalififacties leeftijd voor vergelijking met input
            // inputage is hier zodat je het kan vergelijken met de agerating in json
            // reden dat het dit getal is is omdat niets in de json een agerating heeft van -513892518

            for (int i = 0 ; i < numbersInputArray.Length ; i++)
            {
               if  (this.InputSearch == numbersInputArray[i])
               {
                    inputAge = Convert.ToInt32(numbersInputArray[i]);
                    // input naam veranderd voor leeftijdskwalificatie
               }
            }
            // .ToLower() maakt alles kleine letters om problemen te voorkomen tijdens het vergelijken
            for (int i = 0; i < filmList.FilmArray.Length; i++)
            {
                if (this.InputSearch.ToLower() == filmList.FilmArray[i].Name.ToLower())
                {
                    returnList.Add(filmList.FilmArray[i]);
                }

                else if (this.InputSearch.ToLower() == filmList.FilmArray[i].Director.ToLower())
                {
                    returnList.Add(filmList.FilmArray[i]);
                }

                for (int j = 0; j < filmList.FilmArray[i].Genres.Length; j++)
                {
                    if (this.InputSearch.ToLower() == filmList.FilmArray[i].Genres[j].ToLower())
                    {
                        returnList.Add(filmList.FilmArray[i]);
                    }
                }

                if (inputAge == filmList.FilmArray[i].AgeRating)
                {
                    returnList.Add(filmList.FilmArray[i]);
                }
            } 
            return returnList;
        }

        // check om te kijken of er wel films aan de lijst zijn toegevoegd
        // zo niet geeft een error bericht
        // zo ja returned deze de lijst
        public string FilmLengthCheck(List<Film> filmList)
        {
            string returnString = "";
            if (filmList.Count == 0)
            {
                return "Er is niets gevonden wat overeenkomt";
            }
            else
            {
                for (int i = 0; i < filmList.Count; i++)
                {
                    returnString = returnString + $"naam: {filmList[i].Name}, regisseur: {filmList[i].Director}, leeftijdskwalificatie: {filmList[i].AgeRating}";
                    returnString = returnString + "\n";
                }
            }
            return returnString;
        }
    }
}
