using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Good
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GoodId { get; set; } 

        public int GoodSeq {get;set;}
        public string GoodStore { get; set; }
        public string GoodDimensions { get; set; }
        public string GoodName { get; set; }
        // public string GoodUnit {get;set;} 
        public int GoodQuentity { get; set; }
        public double UnitPrice { get; set; }
        public double SumPrice { get; set; } 

        public string Currency {get;set;}

        public string CustomerAcctNum {get;set;}

        public string MerchantAcctNum {get;set;}
        public string BillId { get; set; } 

        public string BillSeq {get;set;}
        // public Bill Bill { get; set; }

    }
}