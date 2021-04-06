using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class SampleImageModel
    {
        public string QuoteText { get; set; }
        public string Position { get; set; }
        public string ImageName { get; set; }
        public int FontSize { get; set; }
        public string FontColor { get; set; }
        public IFormFile ImageContent { get; set; }
    }
}
