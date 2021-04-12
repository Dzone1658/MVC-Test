using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Employee_CRUD.Bll
{
    public class QuotesBll : BaseRepository<TBL_PublicPost>, IQuotesBll
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;


        public QuotesBll(DataContext context, IConfiguration configuration, IHttpContextAccessor contextAccessor) : base(context)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public ResultBase<List<PostViewModel>> GetAllPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new();
            try
            {
                PostList = GetAll().Where(x => x.IsActive = true).Select(x => new PostViewModel
                {
                    PostID = x.PostID,
                    ImageName = x.ImageName,
                    PostCategory = x.PostID,
                    QuoteText = x.QuoteText,
                    Tags = x.Tags,
                    UserName = x.UserID.ToString()
                }).ToList();
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

        public ResultBase<ManagePostModel> Upsert(ManagePostModel managePostModel)
        {
            var result = new ResultBase<ManagePostModel> { IsSuccess = false };
            string UserGUIDString = _contextAccessor.HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(UserGUIDString))
            {
                int StartIndex = UserGUIDString.IndexOf(":");
                UserGUIDString = UserGUIDString.Substring(StartIndex + 1).Trim();
            }
            try
            {
                TBL_PublicPost publicPost = new TBL_PublicPost
                {
                    ImageName = managePostModel.ImageName,
                    CategoryID = managePostModel.PostCategory,
                    PostedDateTime = DateTime.Now,
                    QuoteText = managePostModel.QuoteText,
                    Tags = managePostModel.Tags,
                    IsActive = true,
                    UserID = UserGUIDString
                };
                Add(publicPost);
                Save();
                result.Result = managePostModel;
                result.Message = "Your Record has been Saved";
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wring while Saving record. Please try again letter." + ex.Message;
            }

            return result;
        }
    }
}