using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class Download
    {
        public int DownloadID { get; set; }
        public string NoteID { get; set; }
        public string Seller { get; set; }
        public string Downloader { get; set; }
        public bool IsSellerHasAllowedDownload { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsAttachmentDownloaded { get; set; }
        public DateTime? AttachmentDownloadedDate { get; set; }
        public bool IsPaid { get; set; }
        public string PurchasedPrice { get; set; }
        public string NoteTitle { get; set; }
        public string NoteCategory { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
