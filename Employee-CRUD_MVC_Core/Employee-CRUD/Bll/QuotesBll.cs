﻿using System;
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
    public class QuotesBll : BaseRepository<TBL_PublicPost>, IQuotesBll
    {
        private readonly IConfiguration _configuration;
        private readonly ISessionHelper _sessionHelper;

        public QuotesBll(DataContext context, IConfiguration configuration, ISessionHelper sessionHelper) : base(context)
        {
            _configuration = configuration;
            _sessionHelper = sessionHelper;
        }

        public ResultBase<List<PostViewModel>> GetAllPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new();
            try
            {
                string UserGUIDString = _sessionHelper.GetDecodedSession().UserName;
                PostList = GetAll().Where(x => x.IsActive = true).Select(x => new PostViewModel
                {
                    PostID = x.PostID,
                    ImageName = x.ImageName,
                    PostCategory = x.PostID,
                    QuoteText = x.QuoteText,
                    Tags = x.Tags,
                    //UserName = x.UserID.ToString()
                    UserName = UserGUIDString
                }).ToList();
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
                string UserGUIDString = _sessionHelper.GetDecodedSession().UserName;
                PostList = GetAll().Where(x => x.IsActive = true && x.UserID == UserGUIDString).Select(x => new PostViewModel
                {
                    PostID = x.PostID,
                    ImageName = x.ImageName,
                    PostCategory = x.PostID,
                    QuoteText = x.QuoteText,
                    Tags = x.Tags,
                    //UserName = x.UserID.ToString(),
                    UserName = UserGUIDString,
                    PostedDateTime = x.PostedDateTime
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
            string UserGUIDString = _sessionHelper.GetDecodedSession().UserID;
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