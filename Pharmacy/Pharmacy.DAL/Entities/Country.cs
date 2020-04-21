using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }

        public List<Country> Countries { get; set; }
    }
}
