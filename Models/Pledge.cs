// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using System;
// namespace Backend.Models
// {
//     public class Pledge
//     { 
//         [Key]
//         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//         public string ContractId {get;set;}  
//         public string EmployeeName {get;set;} 
//         public string MerchantName {get;set;}   
//         [Column(TypeName = "datetime")]
//         public DateTime Bill1IncertedOn { get; set; }   
//         public string Amount {get;set;} 
//         public string AmountWrite {get;set;} 
//         public string NumberOfInstallments {get;set;} 
//         public string Customer {get;set;} 
//         public string MerchantAccountNumber {get;set;} 

//         public string BillId {get;set;} 
//         public Bill Bill {get;set;}




//     }
// }