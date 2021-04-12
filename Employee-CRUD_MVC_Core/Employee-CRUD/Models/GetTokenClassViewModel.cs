using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class GetTokenClassViewModel
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string LoginModel { get; set; }
    }
}
