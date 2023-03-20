using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class MerchantRegisterViewModel
    {
        // [Required]
        // public string MerchantName { get; set; }
        [Required]
        public string MerchantAccountNumber { get; set; } 
        [Required]
        public string MerchantIBAN { get; set; } 
        [Required]
        public string MerchantPhoneNumber { get; set; }
     
        public string MerchantBranch { get; set; }
     
        public string MerchantDepartment { get; set; }
        [Required]
        public string BussinessName { get; set; }
        [Required]
        public string MerchantPassword { get; set; }
        [Required]
        public string MerchantConfirmPassword { get; set; }


    }
}
