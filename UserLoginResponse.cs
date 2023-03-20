using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class UserLoginResponse: UserMangageResponse
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string PhoneNumber { get; set; } 
        public string IBAN { get; set; } 
        public string BussinessName {get;set;}
       // public string Email { get; set; }
        public string Role { get; set; }

    }
}
