using Employee_CRUD.Bll.Interface;
using Employee_CRUD.Filter;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class HomeController : Controller
    {
        private readonly IQuotesBll _quotesBll;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IQuotesBll quotesBll, IHostingEnvironment hostingEnvironment)
        {
            _quotesBll = quotesBll;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(_quotesBll.GetAllPosts().Result);
        }

        public async Task<IActionResult> DownloadFiles(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            //var path = Path.Combine(_hostingEnvironment.WebRootPath, @"Assets/Image/UserUploadedImages/"+filename);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Assets\\Image\\QuotesData", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, Utils.Utils.GetContentType(path), Path.GetFileName(path));
        }

        public async Task<JsonResult> ShortenLink(string FileName)
        {
            string URL = "https://cutt.ly/api/api.php";
            FileName = "https://codeisall.com/jquery-datatables-in-mvc/";
            string ShortenLink = string.Empty;
            string urlParameters = "?key=ec5cf0d7189303d33d4dd3fbdf68c9629f6bc&short=" + FileName;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsStringAsync();  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var responseResult = JsonConvert.DeserializeObject<JToken>(await dataObjects);
                    ShortenLink = responseResult["url"]["shortLink"].ToString();
                }
            }
            return Json(new { ShortenLink });
        }
    }
}