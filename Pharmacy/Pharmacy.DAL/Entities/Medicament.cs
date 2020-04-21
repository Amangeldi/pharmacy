using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    /// <summary>
    /// Лекарственный препарат
    /// </summary>
    public class Medicament
    {
        public int Id { get; set; }
        /// <summary>
        /// Производитель лекарства
        /// </summary>
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<MedicamentLangLink> medicamentLangLinks { get; set; }
        
    }
}
