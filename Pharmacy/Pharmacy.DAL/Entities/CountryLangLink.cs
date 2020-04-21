using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class CountryLangLink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int LangId { get; set; }
        public Lang Lang { get; set; }
    }
}
