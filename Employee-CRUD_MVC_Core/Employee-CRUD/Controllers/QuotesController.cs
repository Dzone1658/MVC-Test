using Employee_CRUD.Bll;
using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Filter;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    //[TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class QuotesController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IQuotesBll _quotesBll;
        private readonly ITagsBll _tagsBll;
        private readonly ICategoryBll _categoryBll;

        [Obsolete]
        public QuotesController(IHostingEnvironment hostingEnvironment, IQuotesBll quotesBll, ICategoryBll categoryBll, ITagsBll tagsBll)
        {
            _hostingEnvironment = hostingEnvironment;
            _quotesBll = quotesBll;
            _categoryBll = categoryBll;
            _tagsBll = tagsBll;
        }

        public IActionResult Index()
        {
            ViewBag.Category = _categoryBll.GetAllCategoriesDropDown();
            ViewBag.Tags = _tagsBll.GetAllTagsByUserID();
            ViewBag.QuotesPosition = new List<SelectListItem>
            {
                  new SelectListItem { Text = "Top Left", Value = "Top Left"},
                  new SelectListItem { Text = "Top Center", Value = "Top Center"},
                  new SelectListItem { Text = "Top Right", Value = "Top Right"},
                  new SelectListItem { Text = "Center", Value = "Center", Selected=true},
                  new SelectListItem { Text = "Bottom Left", Value = "Bottom Left"},
                  new SelectListItem { Text = "Bottom Center", Value = "Bottom Center"},
                  new SelectListItem { Text = "Bottom Right", Value = "Bottom Right"}
            };
            return View();
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> ManageQuotes(ManagePostModel sampleImageModel)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, @"Assets/Image/UserUploadedImages/");
            string fileName = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(sampleImageModel.ImageContent.FileName);
            var FileSaveLocation = path + fileName;
            if (sampleImageModel.ImageContent != null)
            {
                using (Stream fileStream = new FileStream(FileSaveLocation, FileMode.Create))
                {
                    await sampleImageModel.ImageContent.CopyToAsync(fileStream);
                }
            }
            else
            {
                //Select existing uploaded image
            }
            sampleImageModel.ImageName = DrawOverImageBll.DrawTextOverImage(sampleImageModel.QuoteText, FileSaveLocation, sampleImageModel.Position, sampleImageModel.FontColor, sampleImageModel.FontSize);
            _quotesBll.Upsert(sampleImageModel);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public JsonResult DeletePostByPostID(int PostId)
        {
            var result = _quotesBll.DeletePostByPostID(PostId);
            return Json(new { result });
        }


        public ActionResult GetUserPost(DataTablesModel search)
        {
            var UserPostData = _quotesBll.GetAllUserPosts();

            var totalCount = UserPostData.Result.Count();
            if (search.iDisplayStart >= 0 && search.iDisplayLength > 0)
            {
                UserPostData.Result = UserPostData.Result.OrderByDescending(x => x.PostedDateTime).Skip(search.iDisplayStart).Take(search.iDisplayLength).ToList();
            }

            return Json(new DataTablesResponse<PostViewModel>(UserPostData.Result, totalCount, totalCount, search.sEcho));
        }

    }
}