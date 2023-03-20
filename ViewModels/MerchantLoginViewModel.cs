using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class MerchantLoginViewModel
    {
        [Required]
        public string MerchantPhoneNumber { get; set; }
        [Required]
        public string MerchantPassword { get; set; }
    }
}
