using Employee_CRUD.Bll;
<<<<<<< HEAD
using Employee_CRUD.Filter;
=======
using Employee_CRUD.Bll.Interface;
>>>>>>> 26bd60ac490337768f739f54e63d7ec356a5ae87

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
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