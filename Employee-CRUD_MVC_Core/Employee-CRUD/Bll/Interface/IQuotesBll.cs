﻿using System.Collections.Generic;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Models;

namespace Employee_CRUD.Bll.Interface
{
    public interface IQuotesBll
    {
        ResultBase<List<string>> DeletePostByPostID(int PostID);

        ResultBase<List<PostViewModel>> GetAllPosts();

        ResultBase<List<PostViewModel>> GetAllUserPosts();

        ResultBase<ManagePostModel> AddUpdatePublicPost(ManagePostModel managePostModel);
    }
}