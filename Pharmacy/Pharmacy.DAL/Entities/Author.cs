using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.Entities
{
    public class Author : User
    {
        public List<News> News { get; set; }
    }
}
