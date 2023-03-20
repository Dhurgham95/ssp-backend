using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class AskBankViewModel
    {
        public string RelatedCustomerName {get;set;}  
        public string RelatedPhoneNumber {get;set;}
        public string AccountNumberRequestInfo { get; set; }
        public DateTime RequestInfoDateTime { get; set; }
        public string RequestAnswer { get; set; }

        public DateTime RequestAnswerDateTime { get; set; }
     
        
    }
}
