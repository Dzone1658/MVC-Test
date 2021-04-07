using System;
using System.IO;
using System.Threading.Tasks;

using Employee_CRUD.Bll;
using Employee_CRUD.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Employee_CRUD.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IQuotesBll _quotesBll;

        public QuotesController(IHostingEnvironment hostingEnvironment, IQuotesBll quotesBll)
        {
            _hostingEnvironment = hostingEnvironment;
            _quotesBll = quotesBll;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
            var result =_quotesBll.Upsert(sampleImageModel);
            //Return Json with msg and redirect to same page with newly added post.
            string strPhoto = (@"Assets/Image/QuotesData/" + sampleImageModel.ImageName);
            return File(strPhoto, "image/png");
        }
    }
}