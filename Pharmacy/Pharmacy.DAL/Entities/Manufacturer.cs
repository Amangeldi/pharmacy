using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public List<Manufacturer> Manufacturers { get; set; }
    }
}
