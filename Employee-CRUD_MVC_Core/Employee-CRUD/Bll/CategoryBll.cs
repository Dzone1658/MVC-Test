using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_CRUD.Bll
{
    public class CategoryBll: ICategoryBll
    {
        private readonly IConfiguration _configuration;
        public CategoryBll(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<SelectListItem> GetAllCategoriesDropDown()
        {
            List<SelectListItem> ListOfCategories = new();
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand("PR_GET_Categories", sqlConnection))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem Category = new();
                                if (!reader["PostCategoryName"].Equals(DBNull.Value))
                                    Category.Text = Convert.ToString(reader["PostCategoryName"]);
                                if (!reader["CategoryID"].Equals(DBNull.Value))
                                    Category.Value = Convert.ToString(reader["CategoryID"]);
                                ListOfCategories.Add(Category);
                            }
                        }
                    }
                }
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
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand("PR_GET_Categories", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TBL_Category tBL_Category = new();
                                if (!reader["CategoryID"].Equals(DBNull.Value))
                                    tBL_Category.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                                if (!reader["IsActive"].Equals(DBNull.Value))
                                    tBL_Category.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                if (!reader["PostCategoryName"].Equals(DBNull.Value))
                                    tBL_Category.PostCategoryName = Convert.ToString(reader["PostCategoryName"]);
                                ListOfCategories.Add(tBL_Category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfCategories;
        }
    }
}
