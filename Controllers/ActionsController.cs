using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.PartialModels;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.IO;
using Microsoft.Extensions.Hosting;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly DataContext _db;
        public IHostEnvironment env;
       // private readonly IUserService _userService;
        public ActionsController(DataContext db, IHostEnvironment en )
        {
            _db = db; 
            env = en;
          //  _userService = userService;
        }  

        [HttpPost("RejectBill")]

        public ActionResult<Bill> RejectBill([FromQuery]NoteRejection noteRejection)
        {
            Bill billToReject = _db.Bills.FirstOrDefault(b => b.BillSeq == noteRejection.BillSeq); 
            if(billToReject != null) 
            {
                billToReject.NoteForRejection = noteRejection.NoteForRejection ;  
                billToReject.NoteForRejectionDate = DateTime.Now;
                  _db.SaveChanges();
                return billToReject;
            } 
            return null;

        }   


        [HttpPost("RejectBillInsurance")]

        public ActionResult<Bill> RejectBillInsurance([FromQuery]NoteRejection noteRejection)
        {
            Bill billToReject = _db.Bills.FirstOrDefault(b => b.BillSeq == noteRejection.BillSeq); 
            if(billToReject != null) 
            {
                billToReject.NoteForInsuranceRejection = noteRejection.NoteForRejection ; 
                billToReject.NoteForInsuranceRejectionDate = DateTime.Now;
                  _db.SaveChanges();
                return billToReject;
            } 
            return null;

        }   

        
        [HttpPost("RejectBillOperation")]

        public ActionResult<Bill> RejectBillOperation([FromQuery]NoteRejection noteRejection)
        {
            Bill billToReject = _db.Bills.FirstOrDefault(b => b.BillSeq == noteRejection.BillSeq); 
            if(billToReject != null) 
            {
                billToReject.NoteForOperationRejection = noteRejection.NoteForRejection ;  
                billToReject.NoteForOperationRejectionDate = DateTime.Now;
                  _db.SaveChanges();
                return billToReject;
            } 
            return null;

        }  




        [HttpPost("ApproveAndSendToInsuranceCompany")]

        public ActionResult<Bill> ApproveAndSendToInsuranceCompany([FromQuery]NoteRejection inputN)
        {
            Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
            if(approvalBill != null) 
            {
                approvalBill.FirstAuditFromCompanyEmployee = true ; 
                approvalBill.FirstAuditFromCompanyEmployeeDate = DateTime.Now;
                approvalBill.NoteOne = inputN.NoteForRejection; 
                approvalBill.NoteOneDate = DateTime.Now; 
                approvalBill.FCNotification = false; 
               // insurance notifications 
                approvalBill.InsuranceComapnyNotification = true;
                _db.SaveChanges();
                return approvalBill;

            } 
            return null; 

        }   



            [HttpPost("ApproveAndSendToManaget")] 
         public ActionResult<Bill> ApproveAndSendToManager([FromQuery] NoteRejection inputN) 
         { 
              Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
              approvalBill.NoteThree = inputN.NoteForRejection; 
              approvalBill.SecondAuditFromCompanyEmployee = true; 
              approvalBill.OperationNoteForOperationsDate = DateTime.Now;  
              approvalBill.DirectiveManagerNotification = true;

              _db.SaveChanges() ; 

              return approvalBill;

         }   

        [HttpPost("RejectCompany2")] 
         public ActionResult<Bill> RejectComapany2([FromQuery] NoteRejection inputN) 
         { 
              Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
              approvalBill.NoteForOperationRejection = inputN.NoteForRejection; 
              approvalBill.SecondAuditFromCompanyEmployee = false; 
              approvalBill.NoteForOperationRejectionDate = DateTime.Now; 

              _db.SaveChanges() ; 

              return approvalBill;

         }  


         [HttpPost("ApproveAndSendToOperations")]

        public ActionResult<Bill> ApproveAndSendToOperations([FromQuery]NoteRejection inputN)
        {
            Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
            PersonalAccount personatAccountToPullBraCode = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == approvalBill.CustomerAcctNumForBill); 
            
            // 670  1 
            // 671  2
            // 672  3 1 
            // 673  4 
            // 674  5 
            // 675  6 
            // 676  7 
            // 999  8  
            Dictionary<string, int> mapBra = new Dictionary<string, int>();
            mapBra.Add("1", 670); 
            mapBra.Add("2", 671 ); 
            mapBra.Add("3", 672 );
            mapBra.Add("4", 673 );
            mapBra.Add("5", 674 );
            mapBra.Add("6", 675 );
            mapBra.Add("7", 676 );
            mapBra.Add("8", 999 );
            if(approvalBill != null) 
            {
                approvalBill.DirectiveManagerApproval = true ;
                approvalBill.DirectiveManagerApprovalDate = DateTime.Now;
                approvalBill.DirectiveManagerApprovalNote = inputN.NoteForRejection; 
                approvalBill.DirectiveManagerApprovalNoteDate = DateTime.Now; 
                
                //txt write 
                  string doc1 = "payment to  insurance company.txt"; 
                  string doc2 = "payment to the profit.txt"; 
                  string doc3 = "payment to vendors.txt";

                  var path = Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc1);
                  using StreamWriter writer = new StreamWriter (path, true); 

                  var path2 = Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
                  using StreamWriter writer2 = new StreamWriter (path2, true); 

                  var path3 = Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3);
                  using StreamWriter writer3 = new StreamWriter (path3, true);




                  DateTime dt = DateTime.Now;
                  string dt2String = dt.ToString("dd/MM/yyyy"); 
                  dt2String = dt2String.Replace("/", ""); 
                  
                //  string braCode = personatAccountToPullBraCode.IdBrn;
                string braCode;

                //  if(mapBra.ContainsKey(personatAccountToPullBraCode.IdBrn.ToString())) 
                //  { 
                     string branchCode = mapBra[personatAccountToPullBraCode.IdBrn.ToString()].ToString() ;  
                     braCode = branchCode; 
                     


                // }
                   string MerchantAcctNumForBill = approvalBill.MerchantAcctNumForBill;
                 //  Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq ); 
                   string customerAcctNum = approvalBill.CustomerAcctNumForBill ; 
                   string insuranceProfit = Math.Round((int.Parse(approvalBill.BillAmount)/1.1) * 0.03).ToString();  
                  // Console.WriteLine(int.Parse(approvalBill.BillAmount)/ 1.1); 
                 //  Console.WriteLine((int.Parse(approvalBill.BillAmount)/1.1) * 0.03);
                   string bankProfitMonthly = Math.Round(((double.Parse(approvalBill.BillAmount) / 1.1) * 0.1) / int.Parse(approvalBill.PayCountForBill))
                                   .ToString();
                     string merchantRevenue = Math.Round((double.Parse(approvalBill.BillAmount)/ 1.1) / int.Parse(approvalBill.PayCountForBill))
                                   .ToString();

                  writer.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,42,{insuranceProfit},payment to insurance");
                  writer.Close(); 
                  writer2.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,40,{bankProfitMonthly},payment to profit");
                  writer2.Close();  
                  writer3.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,41,{merchantRevenue},payment Related Merchant Account Number = {MerchantAcctNumForBill}"); 
                  writer3.Close();
    
                //   writer.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,42,{insuranceProfit},payment to insurance");
                //   writer.Close(); 
                //   writer2.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,40,{bankProfitMonthly},payment to profit");
                //   writer2.Close();  
                //   writer3.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,41,{merchantRevenue},payment Related Merchant Account Number = {MerchantAcctNumForBill}"); 
                //   writer3.Close();

                  approvalBill.OperationsNotification = true;
                 _db.SaveChanges();
                  return approvalBill;

            } 
            return null; 

        }  


        [HttpPost("ManagerRejection")]

        public ActionResult<Bill> ManagerRejectionBill([FromQuery]NoteRejection inputN)
        {
            Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
            if(approvalBill != null) 
            {
                approvalBill.DirectiveManagerApproval = false ;
                approvalBill.DirectiveManagerApprovalDate = DateTime.Now;
                approvalBill.DirectiveManagerRejectionNote = inputN.NoteForRejection; 
                approvalBill.DirectiveManagerRejectionNoteDate = DateTime.Now;
                _db.SaveChanges();
                return approvalBill;

            } 
            return null; 

        } 


        // [HttpPost("ApproveAndSendToOperations")]

        // public ActionResult<Bill> ApproveAndSendToOperations([FromQuery]NoteRejection inputN)
        // {
        //     Bill approvalBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
        //     if(approvalBill != null) 
        //     {
        //         approvalBill.SecondAuditFromCompanyEmployee = true ;
        //         approvalBill.SecondAuditFromCompanyEmployeeDate = DateTime.Now;
        //         approvalBill.NoteThree = inputN.NoteForRejection; 
        //         approvalBill.NoteThreeDate = DateTime.Now;
        //         _db.SaveChanges();
        //         return approvalBill;

        //     } 
        //     return null; 

        // } 

        [HttpPost("OperationsNoteAterOperate")]

        public ActionResult<Bill> OperationNote([FromQuery]NoteRejection inputN)
        {
            Bill noteBill = _db.Bills.FirstOrDefault(b => b.BillSeq == inputN.BillSeq); 
            if(noteBill != null) 
            {
                noteBill.OperationNoteForOperations = inputN.NoteForRejection; 
                noteBill.OperationNoteForOperationsDate = DateTime.Now;
                _db.SaveChanges();
                return noteBill;
            }

            return null;
        }

   

  

     
    }
}