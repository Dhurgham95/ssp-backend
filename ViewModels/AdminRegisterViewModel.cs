using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required]
        public string AdminName { get; set; }
        [Required]
        public string AdminBranch { get; set; }
        [Required]
        public string AdminDepartment { get; set; }
        [Required]
        public string AdminPassword { get; set; }
        [Required]
        public string AdminConfirmPassword { get; set; }
      
    }
}
