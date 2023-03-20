using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.ViewModels
{
    public class Test
    {
        public int PayCount { get; set; } 
        public DateTime Start { get; set; } 
        public DateTime End { get; set; } 
        public double PayAmt { get; set; } 
        public double ThreePercentageFromBillAmt {get;set;} 
        public double MerchanMontlyReturnPay {get;set;} 
        public double FivePercentageOfPayAmt {get;set;} 
       // public double ThreePercentageFromNetMerchant{get;set;} 
        public double ThreePercentageFromEightPerectage {get;set;} 
        public double ThreePercentgeFromPayAmt {get;set;} 

        public double MonthlyInsurancePay {get;set;} 

        public double NetBankGainMontly {get;set;} 

        public double InsuranceAmt {get;set;}


    }
}