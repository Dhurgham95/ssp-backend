using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class InsuranceTimeToPay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string InsuranceTimeToPayId { get; set; }

      
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]

        public DateTime YearOne { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]

        public DateTime YearTwo { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]

        public DateTime YearThree { get; set; }

        public string BillId { get; set; }

        public Bill Bill { get; set; }



    }
}
