using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class MedicamentRegistrationDTO
    {
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public string MedicamentNameENG { get; set; }
        public string DescriptionENG { get; set; }
        public string MedicamentNameRUS { get; set; }
        public string DescriptionRUS { get; set; }
        public string MedicamentNameTKM { get; set; }
        public string DescriptionTKM { get; set; }
    }
}
