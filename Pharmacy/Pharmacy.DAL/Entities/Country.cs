using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public List<Manufacturer> Manufacturers { get; set; }
        public List<CountryLangLink> CountryLangLinks { get; set; }
    }
}
