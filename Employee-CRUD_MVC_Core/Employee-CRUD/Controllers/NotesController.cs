using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Employee_CRUD.Models;

using Microsoft.AspNetCore.Mvc;

namespace Employee_CRUD.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            List<NoteUnderReviewViewModel> noteUnderReviewViewModelList = new List<NoteUnderReviewViewModel>
            { new NoteUnderReviewViewModel{
                Sr_No = 1,
                Seller = "Dzone",
                Status = "Approved",
                Category = "IT",
                NoteTitle = "Software Engineer",
                DateAdded = DateTime.Now,
                Action = "" },
            { new NoteUnderReviewViewModel{
                Sr_No = 2,
                Seller = "Dj",
                Status = "InReview",
                Category = "IT",
                NoteTitle = "Business Analysis",
                DateAdded = DateTime.Now,
                Action = ""}
            } };
            return View(noteUnderReviewViewModelList);
        }
    }
}
