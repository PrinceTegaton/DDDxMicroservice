using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Models
{
    public class UserProfile : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }        
    }
}
