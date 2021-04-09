using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;
using BolierPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Employee_CRUD.Bll
{
    public interface IQuotesBll : IBaseRepository<TBL_PublicPost>
    {
        //bool DeletePostById(int id);
        ResultBase<List<string>> DeletePostByPostID(int PostID);

        ResultBase<List<PostViewModel>> GetAllPosts();

        ResultBase<ManagePostModel> Upsert(ManagePostModel managePostModel);
    }

    public class QuotesBll : BaseRepository<TBL_PublicPost>, IQuotesBll
    {
        private readonly IConfiguration _configuration;

        public QuotesBll(DataContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        //public bool DeletePostById(int id)
        //{
        //}

        public ResultBase<List<PostViewModel>> GetAllPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new List<PostViewModel>();
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
                result.Message = "Something went wrong please try again letter";
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
                result.Message = "Something went wrong please try again letter";
            }

            return result;
        }

        public ResultBase<ManagePostModel> Upsert(ManagePostModel managePostModel)
        {
            var result = new ResultBase<ManagePostModel> { IsSuccess = false };
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
                    UserID = 1
                };
                Add(publicPost);
                Save();
                result.Result = managePostModel;
                result.Message = "Your Record has been Saved";
            }
            catch (Exception ex)
            {
                result.Message = "Something Went Wring while Saving record. Please try again letter.";
            }

            return result;
        }
    }
}