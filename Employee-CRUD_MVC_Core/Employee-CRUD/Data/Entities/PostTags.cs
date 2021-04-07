using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data.Entities
{
    public class TBL_PostTags : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostTagsID { get; set; }
        public int TagID { get; set; }
        public int PostID { get; set; }
    }
}
