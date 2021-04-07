using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class ManagePostModel
    {
        public string QuoteText { get; set; }
        public string Position { get; set; }
        public string Tags { get; set; }
        public int FontSize { get; set; }
        public string FontColor { get; set; }
        public int PostCategory { get; set; }
        public IFormFile ImageContent { get; set; }
        public string ImageName { get; set; }
    }
}
