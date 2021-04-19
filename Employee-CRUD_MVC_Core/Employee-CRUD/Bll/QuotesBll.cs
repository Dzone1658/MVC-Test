using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;
using Employee_CRUD.Utils.Interface;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Employee_CRUD.Bll
{
    public class QuotesBll : IQuotesBll
    {
        private readonly IConfiguration _configuration;
        private readonly ISessionHelper _sessionHelper;
        private readonly ICategoryBll _categoryBll;
        private readonly ITagsBll _tagsBll;

        public QuotesBll(IConfiguration configuration, ISessionHelper sessionHelper, ITagsBll tagsBll, ICategoryBll categoryBll)
        {
            _configuration = configuration;
            _sessionHelper = sessionHelper;
            _categoryBll = categoryBll;
            _tagsBll = tagsBll;
        }

        public ResultBase<List<PostViewModel>> GetAllPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new();
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //Change SP Name
                    using (SqlCommand cmd = new SqlCommand("PR_GET_PublicPost", sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int CategoryID = 0;
                                PostViewModel postViewModel = new();
                                if (!reader["PostID"].Equals(DBNull.Value))
                                    postViewModel.PostID = Convert.ToInt32(reader["PostID"]);
                                if (!reader["ImageName"].Equals(DBNull.Value))
                                    postViewModel.ImageName = Convert.ToString(reader["ImageName"]);
                                if (!reader["QuoteText"].Equals(DBNull.Value))
                                    postViewModel.QuoteText = Convert.ToString(reader["QuoteText"]);
                                if (!reader["PostedDateTime"].Equals(DBNull.Value))
                                    postViewModel.PostedDateTime = Convert.ToDateTime(reader["PostedDateTime"]);
                                if (!reader["UserName"].Equals(DBNull.Value))
                                    postViewModel.UserName = Convert.ToString(reader["UserName"]);
                                if (!reader["CategoryID"].Equals(DBNull.Value))
                                    CategoryID = Convert.ToInt32(reader["CategoryID"]);

                                postViewModel.PostCategory = _categoryBll.GetAllCategories().Where(x => x.CategoryID == CategoryID).FirstOrDefault().PostCategoryName;
                                postViewModel.Tags = _tagsBll.GetAllTagsByPostID(postViewModel.PostID).Select(x => x.TagName).ToList();

                                PostList.Add(postViewModel);
                            }
                        }

                    }
                }

                PostList = PostList.OrderByDescending(x => x.PostedDateTime).ToList();
            }
            catch (Exception ex)
            {
                result.Message = "Something went wrong please try again letter" + ex.Message;
            }
            result.Result = PostList;

            return result;
        }

        public ResultBase<List<PostViewModel>> GetAllUserPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new();
            try
            {
                string UserGUIDString = _sessionHelper.GetDecodedSession().UserID;

                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    //Change SP Name
                    using (SqlCommand cmd = new SqlCommand("PR_GET_PublicPost", sqlConnection))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", UserGUIDString);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int CategoryID = 0;
                                PostViewModel postViewModel = new();
                                if (!reader["PostID"].Equals(DBNull.Value))
                                    postViewModel.PostID = Convert.ToInt32(reader["PostID"]);
                                if (!reader["ImageName"].Equals(DBNull.Value))
                                    postViewModel.ImageName = Convert.ToString(reader["ImageName"]);
                                if (!reader["QuoteText"].Equals(DBNull.Value))
                                    postViewModel.QuoteText = Convert.ToString(reader["QuoteText"]);
                                if (!reader["PostedDateTime"].Equals(DBNull.Value))
                                    postViewModel.PostedDateTime = Convert.ToDateTime(reader["PostedDateTime"]);
                                if (!reader["UserName"].Equals(DBNull.Value))
                                    postViewModel.UserName = Convert.ToString(reader["UserName"]);
                                if (!reader["CategoryID"].Equals(DBNull.Value))
                                    CategoryID = Convert.ToInt32(reader["CategoryID"]);

                                postViewModel.PostCategory = _categoryBll.GetAllCategories().Where(x => x.CategoryID == CategoryID).FirstOrDefault().PostCategoryName;
                                postViewModel.Tags = _tagsBll.GetAllTagsByPostID(postViewModel.PostID).Select(x => x.TagName).ToList();

                                PostList.Add(postViewModel);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something went wrong please try again letter" + ex.Message;
            }
            result.Result = PostList;

            return result;
        }

        public ResultBase<List<string>> DeletePostByPostID(int PostID)
        {
            var result = new ResultBase<List<string>> { IsSuccess = false };
            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand("PR_Post_PublicPost_Delete", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PostID", PostID);
                        int status = cmd.ExecuteNonQuery();
                        if (status > 0)
                        {
                            result.Message = "Post Deleted Successfully";
                            result.IsSuccess = true;
                        }
                        else
                            result.Message = "Something went wrong please try again letter";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something went wrong please try again letter" + ex.Message;
            }

            return result;
        }

        public ResultBase<ManagePostModel> AddUpdatePublicPost(ManagePostModel managePostModel)
        {
            var result = new ResultBase<ManagePostModel> { IsSuccess = false };
            var Session = _sessionHelper.GetDecodedSession();

            try
            {
                using (SqlConnection sqlConnection = Utils.Utils.GetConnection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand("PR_POST_PublicPost_Insert", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ImageName", managePostModel.ImageName);
                        cmd.Parameters.AddWithValue("@CategoryID", managePostModel.PostCategory);
                        cmd.Parameters.AddWithValue("@PostedDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@QuoteText", managePostModel.QuoteText);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@UserName", Session.UserName);
                        cmd.Parameters.AddWithValue("@UserID", Session.UserID);
                        int PostID = cmd.ExecuteNonQuery();
                        if (PostID > 0)
                        {
                            _tagsBll.AddEditTags(managePostModel.Tags, PostID);
                            result.Result = managePostModel;
                            result.IsSuccess = true;
                            result.Message = "Your Record has been Saved";
                        }

                        else
                            result.Message = "Something went wrong please try again letter";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wring while Saving record. Please try again letter." + ex.Message;
            }
            return result;
        }
    }
}