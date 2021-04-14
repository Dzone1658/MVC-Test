using System.Collections.Generic;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

namespace Employee_CRUD.Bll.Interface
{
    public interface IQuotesBll : IBaseRepository<TBL_PublicPost>
    {
        ResultBase<List<string>> DeletePostByPostID(int PostID);

        ResultBase<List<PostViewModel>> GetAllPosts();

        ResultBase<List<PostViewModel>> GetAllUserPosts();

        ResultBase<ManagePostModel> Upsert(ManagePostModel managePostModel);
    }
}