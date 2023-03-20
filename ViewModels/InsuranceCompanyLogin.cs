using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class InsuranceCompanyLogin
    {
        [Required] 
        public string PhoneNumber {get;set;} 
        [Required] 
        public string Password {get;set;} 
     
    }
}