using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class SuperAdminRegisterViewModel
    {
      //  [Required]
       // public string SuperAdminName { get; set; }
        [Required]
        public string SuperAdminCode { get; set; }
        [Required]
        public string SuperAdminPassword { get; set; }
        [Required]
        public string SuperAdminConfirmPassword { get; set; }
        [Required] 
        public string SuperAdminSecretCodeOne { get; set; }
        [Required] 
        public string SuperAdminSecretCodeTwo { get; set; }

    }
}
