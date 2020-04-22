using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class CategoryLangDTO
    {
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LangId { get; set; }
    }
}
