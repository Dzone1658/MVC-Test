using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class SystemConfiguration
    {
        public int SystemConfigurationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
