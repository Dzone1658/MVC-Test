using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_CRUD.Bll
{
    public class CategoryBll: BaseRepository<TBL_Category>,ICategoryBll
    {
        public CategoryBll(DataContext context) : base(context)
        {
        }
        public List<SelectListItem> GetAllCategoriesDropDown()
        {
            List<SelectListItem> ListOfCategories = new();
            try
            {
                ListOfCategories = GetAll().Where(x => x.IsActive = true).Select(x => new SelectListItem
                {
                    Text = x.PostCategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfCategories;
        }

        public List<TBL_Category> GetAllCategories()
        {
            List<TBL_Category> ListOfCategories = new();
            try
            {
                ListOfCategories = GetAll().Where(x => x.IsActive = true).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfCategories;
        }
    }
}
