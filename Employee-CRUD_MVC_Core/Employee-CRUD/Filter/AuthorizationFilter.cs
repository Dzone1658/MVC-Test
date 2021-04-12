using Employee_CRUD.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

using System;

namespace Employee_CRUD.Filter
{
    public class CustomAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValid = false;
            string UserGUIDString = Convert.ToString(context.HttpContext.Session.GetString("UserID"));
            if (!string.IsNullOrEmpty(UserGUIDString))
            {
                int StartIndex = UserGUIDString.IndexOf(":");
                UserGUIDString = UserGUIDString.Substring(StartIndex + 1).Trim();
                Guid guidResult = Guid.Parse(UserGUIDString);
                isValid = Guid.TryParse(UserGUIDString, out guidResult);
            }

            if (!isValid)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
