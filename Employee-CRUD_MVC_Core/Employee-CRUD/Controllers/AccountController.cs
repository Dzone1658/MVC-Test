using BoilerPlate.Data.Models;
using BoilerPlate.Model.ViewModel;

using Employee_CRUD.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    public class AccountController : Controller
    {
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
        public async Task<IActionResult> Login(Models.LoginViewModel loginViewModel)
        {
            string apiUrl = "https://localhost:44355/api/Auth/Login";
            using (HttpClient client = new HttpClient())
            {
                var parameters = new Dictionary<string, string> { { "username", loginViewModel.Email }, { "password", loginViewModel.Password } };
                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiResultBase<string>>(data);
                    if (result.IsSuccess)
                    {
                        //Role, UserID, UserName from token
                        string Token = string.Empty;
                        var ResultString = result.Result.ToString();
                        var StartIndexValue = ResultString.IndexOf("=") + 1;
                        var EndIndexValue = ResultString.IndexOf(", ");
                        if (StartIndexValue != -1 && EndIndexValue != -1)
                        {
                            Token = result.Result.Substring(StartIndexValue, EndIndexValue - StartIndexValue).Trim();
                        }
                        GetDecodedToken(Token);
                        return RedirectToAction("Index", "Home");
                    }

                    else
                        ModelState.AddModelError("", result.Message);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user name or password");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel registerViewModel)
        {
            string apiUrl = "https://localhost:44355/api/Auth/RegisterUser";
            using (HttpClient client = new HttpClient())
            {
                var parameters = new Dictionary<string, string> { { "username", registerViewModel.Username }, { "password", registerViewModel.Password },{ "Email",registerViewModel.Email}, { "Role", "User" } };
                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiResultBase<ApplicationUser>>(data);
                    if (result.IsSuccess)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                        ModelState.AddModelError("", result.Message);
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong please try again letter");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(RegisterViewModel registerViewModel)
        {
            string apiUrl = "https://localhost:44355/api/Auth/ResetPasswordRequest?UserEmail="+registerViewModel.Email;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiWithoutResultBase>(data);
                    if (result.IsSuccess)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                        ModelState.AddModelError("", result.Message);
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong please try again letter");
                }
            }
            return View();
        }

        protected void GetDecodedToken(string token)
        {
            //Adding Claims Property
            string secret = "onlykeytoaccesstoken";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            HttpContext.Session.SetString("Email", claims.Claims.Where(x => x.Type == "Email").FirstOrDefault().ToString());
            HttpContext.Session.SetString("UserName", claims.Claims.Where(x => x.Type == "UserName").FirstOrDefault().ToString());
            HttpContext.Session.SetString("UserID", claims.Claims.Where(x => x.Type == "UserID").FirstOrDefault().ToString());
            HttpContext.Session.SetString("UserPhone", claims.Claims.Where(x => x.Type == "UserPhone").FirstOrDefault().ToString());
        }

    }
}
