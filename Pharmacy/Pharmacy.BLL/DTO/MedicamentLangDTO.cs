using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class MedicamentLangDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Производитель лекарства
        /// </summary>
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public string MedicamentName { get; set; }
        public string Description { get; set; }
        public int LangId { get; set; }
    }
}
