using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class CategoryRegistrationDTO
    {
        [Required]
        public string NameENG { get; set; }
        [Required]
        public string DescriptionENG { get; set; }
        [Required]
        public string NameRUS { get; set; }
        [Required]
        public string DescriptionRUS { get; set; }
        [Required]
        public string NameTKM { get; set; }
        [Required]
        public string DescriptionTKM { get; set; }
    }
}
