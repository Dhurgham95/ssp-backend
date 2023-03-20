using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class NotPayAlarm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string NotPayAlarmId { get; set; }

        public bool IsNotPay { get; set; } 

        public string CustomerPhoneNumber { get; set; } 

        public string CustomerAccNumber { get; set; }

        public string CustomerName { get; set; }
    
        public IList<NotPayMonth> NotPayMonths { get; set; }  

        public string MerchantAcctNum { get; set; } 

        public string MerchantBussinessName { get; set; }

        public string MerchantAccoutNumber { get; set; }

        public string UserId { get; set; } 

        public User User { get; set; }

    }
}
