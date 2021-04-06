using Employee_CRUD.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public QuotesController(IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ManageQuotes(SampleImageModel sampleImageModel)
        {
            var path = Path.Combine(hostingEnvironment.WebRootPath, @"Assets/Image/UserUploadedImages/");
            string fileName = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(sampleImageModel.ImageContent.FileName);
            var FileSaveLocation = path + fileName;
            using (Stream fileStream = new FileStream(FileSaveLocation, FileMode.Create))
            {
                await sampleImageModel.ImageContent.CopyToAsync(fileStream);
            }

            var ImageName = ImageProcessor.DrawOverImage.DrawTextOverImage(sampleImageModel.QuoteText, FileSaveLocation, sampleImageModel.Position, sampleImageModel.FontColor,sampleImageModel.FontSize);
            string strPhoto = (@"Assets/Image/QuotesData/" + ImageName);
            return File(strPhoto, "image/png");
        }
    }
}
