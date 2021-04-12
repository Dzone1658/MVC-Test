using BoilerPlate.Data.Models;
using BoilerPlate.Model.ViewModel;

using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Bll
{
    public class AccountBll : IAccountBll
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountBll(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<ResultBase<string>> Login(Models.LoginViewModel loginViewModel)
        {
            var result = new ResultBase<string> { IsSuccess = false };

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
                    var responseResult = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiResultBase<string>>(data);
                    if (responseResult.IsSuccess)
                    {
                        string Token = string.Empty;
                        var ResultString = responseResult.Result.ToString();
                        var StartIndexValue = ResultString.IndexOf("=") + 1;
                        var EndIndexValue = ResultString.IndexOf(", ");
                        if (StartIndexValue != -1 && EndIndexValue != -1)
                        {
                            Token = responseResult.Result.Substring(StartIndexValue, EndIndexValue - StartIndexValue).Trim();
                        }
                        GetDecodedToken(Token);
                        result.IsSuccess = responseResult.IsSuccess;
                    }
                    else
                    {
                        result.IsSuccess = responseResult.IsSuccess;
                        result.Message = responseResult.Message;
                    }
                }
                else
                {
                    result.Message = "Something went wrong please try again leter";
                }
            }
            return result;
        }

        public async Task<ResultBase<string>> SignUp(RegisterViewModel registerViewModel)
        {
            var result = new ResultBase<string> { IsSuccess = false };
            string apiUrl = "https://localhost:44355/api/Auth/RegisterUser";
            using (HttpClient client = new HttpClient())
            {
                var parameters = new Dictionary<string, string> { { "username", registerViewModel.Username }, { "password", registerViewModel.Password }, { "Email", registerViewModel.Email }, { "Role", "User" } };
                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiResultBase<ApplicationUser>>(data);
                    if (result.IsSuccess)
                    {
                        result.IsSuccess = responseResult.IsSuccess;
                    }
                    else
                    {
                        result.IsSuccess = responseResult.IsSuccess;
                        result.Message = responseResult.Message;
                    }
                }
                else
                {
                    result.Message = "Something went wrong please try again leter";
                }
            }
            return result;
        }

        public async Task<ResultBase<string>> ResetPassword(string userEmail)
        {
            var result = new ResultBase<string> { IsSuccess = false };
            string apiUrl = "https://localhost:44355/api/Auth/ResetPasswordRequest?UserEmail=" + userEmail;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<BoilerPlate.Model.ViewModel.ApiResultBase<string>>(data);
                    if (result.IsSuccess)
                    {
                        result.IsSuccess = responseResult.IsSuccess;
                    }
                    else
                    {
                        result.IsSuccess = responseResult.IsSuccess;
                        result.Message = responseResult.Message;
                    }
                }
                else
                {
                    result.Message = "Something went wrong please try again leter";
                }
            }
            return result;
        }

        protected void GetDecodedToken(string token)
        {
            var context = _contextAccessor.HttpContext;
            //ToDos get key from app settings
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
            context.Session.SetString("Email", claims.Claims.Where(x => x.Type == "Email").FirstOrDefault().ToString());
            context.Session.SetString("UserName", claims.Claims.Where(x => x.Type == "UserName").FirstOrDefault().ToString());
            context.Session.SetString("UserID", claims.Claims.Where(x => x.Type == "UserID").FirstOrDefault().ToString());
            context.Session.SetString("UserPhone", claims.Claims.Where(x => x.Type == "UserPhone").FirstOrDefault().ToString());
        }


    }
}
