using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PartialPay
    {
        [Key]
        public long PartialPayId { get; set; } 

        public int Num {get;set;}

        public DateTime PartialPayDate {get;set;} 

        public string PartialPayStatus {get;set;} 

        public double PartialPayAmount {get;set;}  

        public string BillSeq {get;set;}

       // public Bill Bill {get;set;}
        
    }
}