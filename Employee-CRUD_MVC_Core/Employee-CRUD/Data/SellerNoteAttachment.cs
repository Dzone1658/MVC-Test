using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class SellerNoteAttachment
    {
        public int SellerNoteAttachmentId { get; set; }
        public int NoteID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
