using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public string Content { get; set; }
        public string Photo { get; set; }
    }
}
