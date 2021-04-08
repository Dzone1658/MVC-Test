using BoilerPlate.Data.Models;

using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
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
                   if(result.IsSuccess) 
                        return RedirectToAction("Index", "Home");
                   else
                        ModelState.AddModelError("", "Invalid user name or password");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user name or password");
                }
            }
            return View();
        }
    }
}
