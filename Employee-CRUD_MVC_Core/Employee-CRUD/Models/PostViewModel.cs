using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class PostViewModel
    {
        public int PostID { get; set; }
        public string QuoteText { get; set; }
        public string Tags { get; set; }
        public string PostCategory { get; set; }
        public string ImageName { get; set; }
        public string UserName { get; set; }
        public DateTime PostedDateTime { get; set; }
    }
}
