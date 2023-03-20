using System;

namespace Backend.PartialModels
{
    public class BillsSearch
    {
        public string MerchantName {get;set;}
        public string StartDate {get;set;}

        public string EndDate {get;set;}

        public string CustomerName {get;set;}

        public string CompanyName {get;set;} 

        public string last24Hours {get;set;} 

        public string RoleForSearch {get;set;}
    }
}