using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Lang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ico { get; set; }

        public List<CountryLangLink> CountryLangLinks { get; set; }
        public List<MedicamentLangLink> MedicamentLangLinks { get; set; }
        public List<NewsLangLink> NewsLangLinks { get; set; }
        public List<CategoryLangLink> CategoryLangLinks { get; set; }
    }
}
