using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class SessionDecodedModel
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
    }
}
