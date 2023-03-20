using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Models
{
    public class OutputValues
    {
        public double MonthlyPay { get; set; }

        public int PayCount { get; set; }

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; } 

        public double InsuranceAmount { get; set; } 

        public double MerchantPayMonthly { get; set; } 

        public double BankRevenueMonthly { get; set; } 

        public bool IsError {get;set;}

        public string ErrorMessege {get;set;}


      
    }
}
