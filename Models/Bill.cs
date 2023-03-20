using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Backend.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BillId { get; set; } 
        public string BillSeq {get;set;}
        public DateTime BillDate { get; set; }
        public string MerchantNameForBill { get; set; }
        public string MerchantAcctNumForBill { get; set; }

        public string MerchantPhoneNumber { get; set; }

        public string MerchantBussinessName { get; set; }

      //  public IList<Good> Goods { get; set; }

        public InsuranceDoc InsuranceDoc { get; set; }

        public InsuranceTimeToPay InsuranceTimeToPay { get; set; }

        public string BillAmount { get; set; }
        // public string BillDiscount { get; set; }


        public string CustomerNameForBill { get; set; }
        public string CustomerAcctNumForBill { get; set; }

        public DateTime CustomerBirth { get; set; }
        public string CustomerAddressForBill { get; set; }
        //    public string BillDelegate {get;set;} 
        // public string BillCashier {get;set;} 
        public string CustomerPhoneNumberForBill { get; set; }
        public string TotalSalaryForBill { get; set; }
        public string PayCountForBill { get; set; }
        public string MonthlyPayAmt { get; set; } 

        public string InsurancePay {get;set;} 
        public string MerchantPay {get;set;}
        public string BankProfit {get;set;}
        
        public DateTime StartPayDateForBill { get; set; }
        public DateTime EndPayDateForBill { get; set; }

        //  public string NetSalaryForBill {get;set;} 

        //  public string ToEquipForBill {get;set;}   

        //  public string NotesForBill {get;set;}


        //  public string RemainingBillAmount {get;set;} 
        //  public string CurrentAmount {get;set;}

        //  public string MaterialDiscount {get;set;}

        //  public string PayAmountBill {get;set;}
        //    public string RemainingBill {get;set;} 
        //  public string ExchangeRate {get;set;}  

        public string BillDoc1 { get; set; }
        public DateTime Bill1IncertedOn { get; set; }

        public string BillDoc2 { get; set; }
        public DateTime Bill2IncertedOn { get; set; }

        public string BillDoc3 { get; set; }
        public DateTime Bill3IncertedOn { get; set; }

        public string BillDoc4 { get; set; }
        public DateTime Bill4IncertedOn { get; set; }

        public string BillDoc5 { get; set; }
        public DateTime Bill5IncertedOn { get; set; }

        public string BillDoc6 { get; set; }
        public DateTime Bill6IncertedOn { get; set; }  

        public string billStatus {get;set;} 

        public DateTime billStatusDate {get;set;} 


        public bool SecondCompanyNotification {get;set;} 

        public bool DirectiveManagerNotification {get;set;} 

        public bool OperationsNotification {get;set;} 

     


    //    public bool FirstAuditFromCompanyEmployee {get;set;} 

      //  public bool 

      public bool FirstAuditFromCompanyEmployee {get;set;}

      public DateTime FirstAuditFromCompanyEmployeeDate {get;set;}
      public bool SecondAuditFromCompanyEmployee {get;set;} 

      public DateTime SecondAuditFromCompanyEmployeeDate {get;set;} 

      public bool IsInsured {get;set;}  

      public DateTime IsInsuredDate {get;set;}

      public string NoteForRejection {get;set;} 

      public DateTime NoteForRejectionDate {get;set;}

      public string NoteForInsuranceRejection {get;set;} 

      public DateTime NoteForInsuranceRejectionDate {get;set;}

      public string NoteForOperationRejection {get;set;}  

      public DateTime NoteForOperationRejectionDate {get;set;}
 
      public string NoteOne {get;set;}  

      public DateTime NoteOneDate {get;set;}

      public string NoteTwo {get;set;} 

      public DateTime NoteTwoDate {get;set;}

      public string NoteThree {get;set;}  

      public DateTime NoteThreeDate {get;set;} 

      public bool DirectiveManagerApproval {get;set;} 

      public DateTime DirectiveManagerApprovalDate {get;set;}

      public string DirectiveManagerApprovalNote {get;set;}  

      public DateTime DirectiveManagerApprovalNoteDate {get;set;}

      public string DirectiveManagerRejectionNote {get;set;} 

      public DateTime DirectiveManagerRejectionNoteDate {get;set;}

      public string OperationNoteForOperations {get;set;} 

      public DateTime OperationNoteForOperationsDate {get;set;}  

      public bool IsOnTmweelFlag {get;set;}  

       public bool FCNotification {get;set;}  

      public bool InsuranceComapnyNotification {get;set;}


      public List<PartialPay> PartialPay {get;set;}
        // public  Good Good {get;set;} 
        // public Pledge Pledge {get;set;} 
        //  public Schedule Schedule {get;set;}
      public string UserId { get; set; }
      public User User { get; set; }  


      

    }
} 

// ;MultipleActiveResultSets=True