using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;
using BolierPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Bll
{
    public interface IQuotesBll : IBaseRepository<TBL_PublicPost>
    {
        void DeletePostById(int id);

        ResultBase<List<PostViewModel>> GetAllPosts();

        ResultBase<ManagePostModel> Upsert(ManagePostModel managePostModel);
    }

    public class QuotesBll : BaseRepository<TBL_PublicPost>, IQuotesBll
    {
        public QuotesBll(DataContext context) : base(context)
        {
        }

        public void DeletePostById(int id)
        {

        }

        public ResultBase<List<PostViewModel>> GetAllPosts()
        {
            var result = new ResultBase<List<PostViewModel>> { IsSuccess = false };
            List<PostViewModel> PostList = new List<PostViewModel>();
            try
            {
                PostList = GetAll().Where(x => x.IsActive = true).Select(x => new PostViewModel
                {
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
