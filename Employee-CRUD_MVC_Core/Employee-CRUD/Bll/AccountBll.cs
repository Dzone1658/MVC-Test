using BoilerPlate.Model.ViewModel;

using Employee_CRUD.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Bll
{
    public class AccountBll
    {
        public ResultBase<string> Login(Models.LoginViewModel loginViewModel)
        {
            var result = new ResultBase<string> { IsSuccess = false };
            return result;
        }

        public ResultBase<Models.LoginViewModel> SignUp(RegisterViewModel registerModel)
        {
            var result = new ResultBase<Models.LoginViewModel> { IsSuccess = false };
            result.Result = new Models.LoginViewModel();
            return result;
        }


    }
}
