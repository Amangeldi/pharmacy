using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Photo { get; set; }

        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public List<NewsLangLink> NewsLangLinks { get; set; }
    }
}
