using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


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
}
