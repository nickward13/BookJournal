using System;
using System.Collections.Generic;

namespace Hectagon.Models
{
    public class Book
    {
        public string Name {get;set;}
        public string Author {get;set;}
        public List<Reading> Readings {get;set;}
    }

    public class Reading
    {
        public DateOnly ReadingDate {get;set;}
        public int Rating {get;set;}
    }
}