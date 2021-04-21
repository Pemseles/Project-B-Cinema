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
    // vraag user input aan (leeftijd, director, naam of genre)
    // maak een for loop aan die door de JSON op zoek gaat
    // wanneer input overeenkomt met iets uit de lijst, return film info ( leeftijdkwalificatie, director, genre, naam )
    // vergeet niet dat alle overeenkomende input weergegeven moet worden
    // optie leeftijdskwalificatie : geef films met overeenkomende leeftijd plus lager > vragen PO? waarschijnlijk zegt hij optie voor allebei vast en zeker, hoe vervelend ook
    // een check maken of nummers in de input zitten > zo ja omzetten naar int

    // string[] numbersInputArray = { "0" , "6" , "9" , "12" , "14" , "16" , "18"};
    // string array met mogelijke kwalififacties leeftijd voor vergelijking met input

    // for (i = 0 ; i < numbersInputArray.Length ; i++){
    // if(inputZoek == numbersInputArray[i]){
    // int inputLeeftijd = Convert.ToInt32(Console.ReadLine());
    // }
    // input naam veranderd voor leeftijdskwalificatie 

    // misschien eerst vragen wat voor type input het is?
    // console writeline is volgens mij standaard string
    // ik bedoel genre/ director/ naam/ leeftijd > anders aan natnael vragen
    // zou kunnen? maar t loopt door t hele json heen en moet de volledige info ophalen

    class Zoekfunctie
    {
        public string InputZoek;

        public Zoekfunctie(string userInput)
        {
            this.InputZoek = userInput;
        }

        public void ZoekMethod()
        {
            this.InputZoek = InputZoek + " testetstetsttettstettste";
        }
    }

}
