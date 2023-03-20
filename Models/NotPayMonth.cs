using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class NotPayMonth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string NotPayMonthId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime Month { get; set; } 

        public string NotPayAlarmId { get; set; } 

        public NotPayAlarm NotPayAlarm { get; set; }

    }
}
