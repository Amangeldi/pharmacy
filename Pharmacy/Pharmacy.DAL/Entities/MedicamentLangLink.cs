using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class MedicamentLangLink
    {
        public int Id { get; set; }
        public string MedicamentName { get; set; }
        public string Description { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
        public int LangId { get; set; }
        public Lang Lang { get; set; }
    }
}
