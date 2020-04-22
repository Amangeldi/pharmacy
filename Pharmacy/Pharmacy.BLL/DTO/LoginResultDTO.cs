using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BLL.DTO
{
    public class LoginResultDTO
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
