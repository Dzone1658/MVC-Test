using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD.Data
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserID { get; set; }
        public DateTime? DOB { get; set; }
        public Gender Gender { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string College { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public enum Gender { Male, Female }
}
