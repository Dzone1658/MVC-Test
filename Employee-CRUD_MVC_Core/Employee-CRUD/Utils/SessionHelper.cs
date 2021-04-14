using Employee_CRUD.Models;
using Employee_CRUD.Utils.Interface;

using Microsoft.AspNetCore.Http;

namespace Employee_CRUD.Utils
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public SessionDecodedModel GetDecodedSession()
        {
            SessionDecodedModel sessionDecodedModel = new();
            int StartIndex = 0;
            string UserGUIDString = _contextAccessor.HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(UserGUIDString))
            {
                StartIndex = UserGUIDString.IndexOf(":");
                sessionDecodedModel.UserID = UserGUIDString.Substring(StartIndex + 1).Trim();
            }
            string UserEmail = _contextAccessor.HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(UserEmail))
            {
                StartIndex = UserEmail.IndexOf(":");
                sessionDecodedModel.Email = UserEmail.Substring(StartIndex + 1).Trim();
            }
            string UserPhone = _contextAccessor.HttpContext.Session.GetString("UserPhone");
            if (!string.IsNullOrEmpty(UserPhone))
            {
                StartIndex = UserPhone.IndexOf(":");
                sessionDecodedModel.UserPhone = UserPhone.Substring(StartIndex + 1).Trim();
            }
            string UserName = _contextAccessor.HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(UserName))
            {
                StartIndex = UserName.IndexOf(":");
                sessionDecodedModel.UserName = UserName.Substring(StartIndex + 1).Trim();
            }
            return sessionDecodedModel;
        }
    }
}
