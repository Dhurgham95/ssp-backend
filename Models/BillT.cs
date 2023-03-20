using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Backend.Models
{
    public class BillT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BillTId { get; set; }
     
        public string MerchantNameForBill { get; set; }
        public string MerchantAcctNumForBill { get; set; }

        public string BillDoc1 { get; set; }
        public DateTime Bill1IncertedOn { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}