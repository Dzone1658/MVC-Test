using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_CRUD.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            SelectListItem selectListItem = new SelectListItem();
            return View();
        }
    }
}
