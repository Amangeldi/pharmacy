using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LangId { get; set; }
    }
}
