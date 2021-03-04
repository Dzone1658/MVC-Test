using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class SellerNote
    {
        public int SellerNoteId { get; set; }
        public int SellerId { get; set; }
        public string Status { get; set; }
        public string ActionedBy { get; set; }
        public string AdminRemarks { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public byte[] DisplayPicture { get; set; }
        public string NoteType { get; set; }
        public string NumberofPages { get; set; }
        public string Description { get; set; }
        public string UniversityName { get; set; }
        public string Country { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string Professor { get; set; }
        public bool IsPaid { get; set; }
        public string SellingPrice { get; set; }
        public string NotesPreview { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
