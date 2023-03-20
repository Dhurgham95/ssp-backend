using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class AskBank
    {
        [Key]
        public long Id { get; set; } 

        public string CustomerNumberRelatedToAccountNumber {get;set;}
        public string AccountNumberRequestInfo { get; set; } 
        public string PhoneNumberRelatedToAccountNumber {get;set;} 
        public DateTime RequestInfoDateTime { get; set; }
        public string RequestAnswer { get; set; }

        public DateTime RequestAnswerDateTime { get; set; } 

        public string Notes {get;set;}
        public string UserId { get; set; }
        public User User {get;set;}
    }
}
