using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class NewsLangLink
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int NewsId { get; set; }
        public News News { get; set; }
        public int LangId { get; set; }
        public Lang Lang { get; set; }
    }
}
