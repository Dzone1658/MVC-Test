﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class SellerNoteReview
    {
        public int SellerNoteReviewId { get; set; }
        public int NoteID { get; set; }
        public int ReportedByID { get; set; }
        public int AgainstDownloadID { get; set; }
        public string Rating { get; set; }
        public string Comments { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
