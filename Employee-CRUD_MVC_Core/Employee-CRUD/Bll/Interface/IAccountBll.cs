using BoilerPlate.Model.ViewModel;

using Employee_CRUD.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Bll.Interface
{
    public interface IAccountBll
    {
        ResultBase<List<string>> Login(Models.LoginViewModel loginViewModel);

        ResultBase<Models.LoginViewModel> SignUp(RegisterViewModel registerModel);

    }
}
