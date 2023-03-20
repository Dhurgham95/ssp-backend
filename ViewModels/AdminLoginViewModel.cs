using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        public string AdminName { get; set; }
      
        [Required]
        public string AdminPassword { get; set; }
     

    }
}
