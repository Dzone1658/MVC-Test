using BoilerPlate.Model.ViewModel;
using Employee_CRUD.Bll.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_CRUD.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountBll _accountBll;

        public AccountController(IAccountBll accountBll)
        {
            _accountBll = accountBll;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserID", string.Empty);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Login(Models.LoginViewModel loginViewModel)
        {
            var result = _accountBll.Login(loginViewModel);

            if (result.Result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", result.Result.Message);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Signup(RegisterViewModel registerViewModel)
        {
            var result = _accountBll.SignUp(registerViewModel);

            if (result.Result.IsSuccess)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", result.Result.Message);
            }
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(RegisterViewModel registerViewModel)
        {
            //ToDos Change model to email string only
            var result = _accountBll.ResetPassword(registerViewModel.Email);

            if (result.Result.IsSuccess)
            {
                return RedirectToAction("Login", "Account");
            }
            else
                ModelState.AddModelError("", result.Result.Message);
            return View();
        }
    }
}
