using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_CRUD.Data.Entities
{
    public class TBL_PublicPost : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }
        [Required]
        public string QuoteText { get; set; }
        public string Tags { get; set; }
        public string ImageName { get; set; }
        public string UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime PostedDateTime { get; set; }
    }
}
