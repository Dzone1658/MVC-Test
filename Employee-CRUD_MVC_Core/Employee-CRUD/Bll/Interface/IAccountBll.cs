using BoilerPlate.Model.ViewModel;

using Employee_CRUD.Models;

using System.Threading.Tasks;

namespace Employee_CRUD.Bll.Interface
{
    public interface IAccountBll
    {
        Task<ResultBase<string>> Login(Models.LoginViewModel loginViewModel);

        Task<ResultBase<string>> SignUp(RegisterViewModel registerModel);
        Task<ResultBase<string>> ResetPassword(string userEmail);

    }
}
