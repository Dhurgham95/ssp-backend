using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } 
        public string Branch { get; set; }
        public string Department { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string BussinessName { get; set; } 
         public IList<AskBank> AskBanks { get; set; } 

        public bool IsOnTmweel { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CurrentTmweelEndingTime { get; set; }

        public PersonalAccount PersonalAccount { get; set; }  

       //  public Bill Bill {get;set;} 
         


    }
}
