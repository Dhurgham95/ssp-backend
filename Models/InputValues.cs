using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Models
{
    public class InputValues
    {
        public int MonthlySalary { get; set; } 

        public double BillAmount { get; set; } 

      //  public double RatioOfSalary { get; set; } 
        public int NumberOfMonths { get; set; } 

        public string CustomerAcctNum {get;set;}
    }
}
