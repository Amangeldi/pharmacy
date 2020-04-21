using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public List<CategoryLangLink> CategoryLangLinks { get; set; }
        public List<Medicament> Medicaments { get; set; }
    }
}
