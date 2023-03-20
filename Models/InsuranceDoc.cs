using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class InsuranceDoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string InsuranceDocId { get; set; }  

        public string BillAmount { get; set; }
        public string MerchantName { get; set; } 
        public string CustomerName { get; set; }
        public string MonthlyPay { get; set; } 
        public string PayCount { get; set; }

        public string InsuranceDoc1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insurance1IncertedOn { get; set; }

        public string InsuranceDoc2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insurance2IncertedOn { get; set; }

        public string InsuranceDoc3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insurance3IncertedOn { get; set; }

        public string BillId { get; set; }

        public Bill Bill { get; set; } 


    }
}
