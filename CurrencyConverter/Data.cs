using System;
using System.Collections.Generic;

namespace CurrencyConverter
{
    class Data
    {
        public DateTime Date { get; set; }

        public List<Valut> valutsList { get; set; }

        public Data(DateTime date)
        {
            Date = date;
            valutsList = new List<Valut>();
        }
        
    }

    class Valut
    {
        public string CharCode { get; }
        public int Nominal { get; }
        public string Name { get; }
        public float Value { get; }

        public Valut()
        {
            CharCode = "RUB";
            Nominal = 1;
            Name = "Российский рубль";
            Value = 1;
        }

        public Valut (string charCode, int nominal, string name, float value)
        {
            CharCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
        }
    }
}
