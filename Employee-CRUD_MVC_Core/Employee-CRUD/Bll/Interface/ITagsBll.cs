using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace Employee_CRUD.Bll.Interface
{
    public interface ITagsBll
    {
        List<SelectListItem> GetAllTagsByUserID();
        void AddEditTags(List<string> TagsList,int PostID);
    }
}
