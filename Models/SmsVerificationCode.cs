using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class SmsVerificationCode
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string VerificationCode { get; set; }

        public string AccountNumber { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime InsetedOn { get; set; }

        public DateTime VerivicationCodeExpireOn { get; set; }

        public bool IsExpired { get; set; }

        public int PersonalAccountIdPa { get; set; }

        //   public PersonalAccount PersonalAccount { get; set; }


    }
}
