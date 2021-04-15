using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Filter;
using Microsoft.AspNetCore.Mvc;

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
        

    }
}