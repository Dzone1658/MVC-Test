using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Utils.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_CRUD.Bll
{
    public class TagsBll : ITagsBll
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IConfiguration _configuration;

        public TagsBll(ISessionHelper sessionHelper, IConfiguration configuration)
        {
            _sessionHelper = sessionHelper;
            _configuration = configuration;
        }

        public List<SelectListItem> GetAllTagsByUserID()
        {
            List<SelectListItem> ListOfTags = new();
            string UserID = _sessionHelper.GetDecodedSession().UserID;
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //Change SP Name
                    using (SqlCommand cmd = new SqlCommand("PR_EMP_Employee_SelectAll", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem Tags = new();
                                if (!reader["TagName"].Equals(DBNull.Value))
                                {
                                    Tags.Text = Convert.ToString(reader["TagName"]);
                                    Tags.Value = Convert.ToString(reader["TagName"]);
                                }
                                ListOfTags.Add(Tags);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfTags;
        }

        public List<TBL_Tags> GetAllTags()
        {
            List<TBL_Tags> ListOfTags = new();
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //Change SP Name
                    using (SqlCommand cmd = new SqlCommand("PR_EMP_Employee_SelectAll", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TBL_Tags tBL_Tags = new();
                                if (!reader["TagID"].Equals(DBNull.Value))
                                    tBL_Tags.TagID = Convert.ToInt32(reader["TagID"]);
                                if (!reader["IsActive"].Equals(DBNull.Value))
                                    tBL_Tags.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                if (!reader["TagName"].Equals(DBNull.Value))
                                    tBL_Tags.TagName = Convert.ToString(reader["TagName"]);
                                ListOfTags.Add(tBL_Tags);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfTags;
        }

        public List<TBL_Tags> GetAllTagsByPostID(int PostID)
        {

            List<TBL_Tags> ListOfTags = new();
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //Change SP Name
                    using (SqlCommand cmd = new SqlCommand("PR_EMP_Employee_SelectAll", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PostID", PostID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TBL_Tags tBL_Tags = new();
                                if (!reader["TagID"].Equals(DBNull.Value))
                                    tBL_Tags.TagID = Convert.ToInt32(reader["TagID"]);
                                if (!reader["IsActive"].Equals(DBNull.Value))
                                    tBL_Tags.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                if (!reader["TagName"].Equals(DBNull.Value))
                                    tBL_Tags.TagName = Convert.ToString(reader["TagName"]);
                                ListOfTags.Add(tBL_Tags);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ListOfTags;

        }

        public void AddEditTags(List<string> TagsList, int postID)
        {
            string UserID = _sessionHelper.GetDecodedSession().UserID;
            var TagsListCopy = TagsList.ToList();
            var AllTags = GetAllTags();
            foreach (var item in TagsListCopy)
            {
                var Tags = AllTags.Where(x => x.TagName.ToLower() == item.ToLower()).FirstOrDefault();
                if (Tags != null)
                {
                    int index = TagsList.FindIndex(a => a == Tags.TagName);
                    TagsList.RemoveAt(index);
                }
            }

            foreach (var item in TagsList)
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //change SP Name
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TagName", item);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        int PostID = cmd.ExecuteNonQuery();
                    }
                }
            }

            AllTags = GetAllTags();

            foreach (var item in TagsListCopy)
            {
                int TagID = AllTags.Where(x => x.TagName.ToLower() == item.ToLower()).FirstOrDefault().TagID;

                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //change SP Name
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TagName", item);
                        cmd.Parameters.AddWithValue("@PostID", postID);
                        cmd.Parameters.AddWithValue("@TagID", postID);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        int PostID = cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}