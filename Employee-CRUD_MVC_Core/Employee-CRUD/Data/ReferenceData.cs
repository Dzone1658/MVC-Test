using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class ReferenceData
    {
        [Key]
        public int ReferenceId { get; set; }
        public string Value { get; set; }
        public string Datavalue { get; set; }
        public string RefCategory { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
