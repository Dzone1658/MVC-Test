using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace Employee_CRUD.Bll.Interface
{
    public interface ITagsBll
    {
        List<SelectListItem> GetAllTagsByUserID();
        List<TBL_Tags> GetAllTags();

        List<TBL_Tags> GetAllTagsByPostID(int PostID);

        void AddEditTags(List<string> TagsList,int PostID);
    }
}
