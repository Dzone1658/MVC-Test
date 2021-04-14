
using Employee_CRUD.Models;

namespace Employee_CRUD.Utils.Interface
{
    public interface ISessionHelper
    {
        public SessionDecodedModel GetDecodedSession();
    }
}
