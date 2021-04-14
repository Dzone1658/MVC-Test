using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace Employee_CRUD.Bll.Interface
{
    public interface ICategoryBll: IBaseRepository<TBL_Category>
    {
        List<SelectListItem> GetAllCategories();
    }
}
