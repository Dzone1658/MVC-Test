using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Models
{
    public class NoteUnderReviewViewModel
    {
        public int Sr_No { get; set; }
        public string NoteTitle { get; set; }
        public string Category { get; set; }
        public string Seller { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
    }

    public enum Action
    {
        Approve,Reject,InReview
    }
}
