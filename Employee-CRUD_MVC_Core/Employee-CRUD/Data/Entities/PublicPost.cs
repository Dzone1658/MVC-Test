using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime PostedDateTime { get; set; }
    }
}
