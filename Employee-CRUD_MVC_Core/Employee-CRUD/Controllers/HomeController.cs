using Employee_CRUD.Bll;
using Employee_CRUD.Bll.Interface;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuotesBll _quotesBll;

        public HomeController(IQuotesBll quotesBll)
        {
            _quotesBll = quotesBll;
        }

        public IActionResult Index()
        {
            return View(_quotesBll.GetAllPosts().Result);
        }

        [HttpGet]
        public JsonResult DeletePostByPostID(int PostId)
        {
            var result = _quotesBll.DeletePostByPostID(PostId);
            return Json(new { result });
        }
    }
}