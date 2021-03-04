using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
