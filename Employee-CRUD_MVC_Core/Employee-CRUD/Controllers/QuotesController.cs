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

        public QuotesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManageQuotes(SampleImageModel sampleImageModel)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, @"Assets/Image/UserUploadedImages/");
            string fileName = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(sampleImageModel.ImageContent.FileName);
            var FileSaveLocation = path + fileName;
            using (Stream fileStream = new FileStream(FileSaveLocation, FileMode.Create))
            {
                await sampleImageModel.ImageContent.CopyToAsync(fileStream);
            }

            var ImageName = DrawOverImageBll.DrawTextOverImage(sampleImageModel.QuoteText, FileSaveLocation, sampleImageModel.Position, sampleImageModel.FontColor, sampleImageModel.FontSize);
            string strPhoto = (@"Assets/Image/QuotesData/" + ImageName);
            return File(strPhoto, "image/png");
        }
    }
}