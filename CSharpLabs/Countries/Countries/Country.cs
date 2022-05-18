using System.Collections.Generic;

namespace Countries
{
    public class Country
    {
        public int Id { get; }
        public string Code { get; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Capital { get; set; }
        public string Continent { get; set; }
        public List<string> Languages { get; set; }

        public Country(int id, string code)
        {
            Id = id;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} Столица:{Capital} Валюта:{Currency} Континент:{Continent}";
        }
    }
}
