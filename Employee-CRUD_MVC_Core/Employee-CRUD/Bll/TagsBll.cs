using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Data.Context;
using Employee_CRUD.Data.Entities;
using Employee_CRUD.Utils.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_CRUD.Bll
{
    public class TagsBll : ITagsBll
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly DataContext _dataContext;

        public TagsBll(DataContext dataContext, ISessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
            _dataContext = dataContext;
        }

        public List<SelectListItem> GetAllTagsByUserID()
        {
            List<SelectListItem> ListOfTags = new();
            string UserID = _sessionHelper.GetDecodedSession().UserID;
            try
            {
                ListOfTags = _dataContext.TBL_PostTags.Where(x => x.UserID == UserID && x.IsActive).Join(_dataContext.TBL_Tags, pt => pt.TagID, x => x.TagID,
                    (pt, x) => new SelectListItem
                    {
                        Text = x.TagName,
                        Value = x.TagName,
                    }).ToList();
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
                ListOfTags = _dataContext.TBL_Tags.Where(x => x.IsActive).ToList();
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
                ListOfTags = _dataContext.TBL_PostTags.Where(x => x.PostID == PostID && x.IsActive).Join(_dataContext.TBL_Tags, pt => pt.TagID, x => x.TagID,
                    (pt, x) => new TBL_Tags
                    {
                        TagID = x.TagID,
                        TagName = x.TagName,
                        IsActive = x.IsActive
                    }).ToList();
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
            var AllTags = _dataContext.TBL_Tags.Where(x => x.IsActive);
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
                TBL_Tags tBL_Tags = new();
                tBL_Tags.IsActive = true;
                tBL_Tags.TagName = item;
                _dataContext.TBL_Tags.Add(tBL_Tags);
                _dataContext.SaveChanges();
            }

            AllTags = _dataContext.TBL_Tags.Where(x => x.IsActive);

            foreach (var item in TagsListCopy)
            {
                TBL_PostTags tBL_PostTags = new();
                tBL_PostTags.IsActive = true;
                tBL_PostTags.PostID = postID;
                tBL_PostTags.TagID = AllTags.Where(x => x.TagName.ToLower() == item.ToLower()).FirstOrDefault().TagID;
                tBL_PostTags.UserID = UserID;
                _dataContext.TBL_PostTags.Add(tBL_PostTags);
                _dataContext.SaveChanges();
            }
        }
    }
}