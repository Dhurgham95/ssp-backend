using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class BranchEmployeeRespondentViewModel
    { 
        [Required]
        public string BranchEmployeeRespondentName { get; set; }
        [Required]
        public string BranchEmployeeRespondentBranch { get; set; }
        [Required]
        public string BranchEmployeeRespondentDepartment { get; set; }
        [Required]
        public string BranchEmployeeRespondentPassword { get; set; }
        [Required]
        public string BranchEmployeeRespondentConfirmPassword { get; set; }
      
        
    }
}