using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class InsuranceCompany
    {
        [Required] 
        public string Name {get;set;} 
        [Required] 
        public string PhoneNumber {get;set;} 
        [Required] 
        public string InsuranceCompanyName {get;set;} 
        [Required] 
        public string Password {get;set;} 
        [Required] 
        public string ConfirmPassword {get;set;}
    }
}