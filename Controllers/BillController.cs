using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



using System.Collections.Generic;
using Backend.PartialModels;
using Backend.ViewModels;
using Backend.Data;
using System.IO;
using System;
using Microsoft.Extensions.Hosting;
using Humanizer; 
using Backend.convert;


 
namespace Backend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {  

        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
         

        private IUserService _userService; 
        private DataContext _db; 

        // private CurrencyInfo _curInfo;

        // private ToWord _toword;


        public IHostEnvironment env;
        
        public BillController(IUserService userService, DataContext db, IHostEnvironment en  )
        {  

            
            _userService = userService;
            _db = db;
            env= en;  
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));

            

        } 

        [HttpPost("BillSend")]

        public ActionResult<string> UploadImages([FromForm] BillT bill)
        {
            try
            {
                var files = HttpContext.Request.Form.Files[0];

                if (files != null)
                {
                   
                        FileInfo fi = new FileInfo(files.FileName);
                        var newfilename = "Image_" + DateTime.Now.TimeOfDay.Seconds + fi.Extension;
                        var path = Path.Combine("", env.ContentRootPath + "\\Images\\" + newfilename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            files.CopyTo(stream);
                        }

                        BillT imageUplaod = new BillT();
                        imageUplaod.BillDoc1 = path;
                      //  imageUplaod.Bill1IncertedOn = DateTime.Now; 
                        imageUplaod.MerchantNameForBill = bill.MerchantNameForBill;
                        imageUplaod.MerchantAcctNumForBill = bill.MerchantAcctNumForBill; 
                        imageUplaod.UserId = bill.UserId;

                        //{
                        //    ImagePath = path,
                        //    CreatedOn = DateTime.Now
                        //};
                        _db.BillTs.Add(imageUplaod);
                        _db.SaveChanges();
                    

                    return "success saved";
                }
                else
                {
                    return "select File";
                }


            }
            catch (Exception e1)
            {
                return e1.Message;

            }
        } 



        
        [HttpPost("BillSend2")]

        public ActionResult<string> SendBill([FromForm] Bill bill)
        {
            try
            {
                var files = HttpContext.Request.Form.Files[0];

                if (files != null)
                {
                   
                        FileInfo fi = new FileInfo(files.FileName);
                        var newfilename = "Image_" + DateTime.Now.TimeOfDay.Seconds + fi.Extension;
                        var path = Path.Combine("", env.ContentRootPath + "\\Images\\" + newfilename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            files.CopyTo(stream);
                        }

                        Bill imageUplaod = new Bill{ 
                              BillDoc1 = path, 
                              Bill1IncertedOn = DateTime.Now,
                              MerchantNameForBill = bill.MerchantNameForBill, 
                              MerchantAcctNumForBill = bill.MerchantAcctNumForBill,  
                              UserId = bill.UserId, 
                        };
                        //imageUplaod.BillDoc1 = path;
                      //  imageUplaod.Bill1IncertedOn = DateTime.Now; 
                       // imageUplaod.MerchantNameForBill = bill.MerchantNameForBill;
                       // imageUplaod.MerchantAcctNumForBill = bill.MerchantAcctNumForBill; 
                       // imageUplaod.UserId = bill.UserId;

                        //{
                        //    ImagePath = path,
                        //    CreatedOn = DateTime.Now
                        //};
                        _db.Bills.Add(imageUplaod);
                        _db.SaveChanges();
                    

                    return "success saved";
                }
                else
                {
                    return "select File";
                }


            }
            catch (Exception e1)
            {
                return e1.Message;

            }
        } 

         [HttpPost("BillS1")]
        public ActionResult<string> SendBillOne([FromForm]Bill bill)
        {

            try
            {
                var billOne = HttpContext.Request.Form.Files[0];
            

                if (billOne != null)
                {
                    

                    FileInfo fi = new FileInfo(billOne.FileName);
                    var billOneName = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fi.Extension;
                    var path = Path.Combine("", env.ContentRootPath + "\\Images\\" + billOneName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        billOne.CopyTo(stream);
                    }

                  
                   
                   
                    //IList<Good> docsP = new IList<Good>();
                    //docsP.Add(docsPath); 

                  //  PartialPay payDetails = new PartialPay(); 
                                      

                 
                    Bill bilToSend = new Bill
                    {
                          BillSeq = bill.BillSeq,
                          BillDate = bill.BillDate,
                          MerchantNameForBill = bill.MerchantNameForBill,
                          MerchantAcctNumForBill  = bill.MerchantAcctNumForBill,
                          MerchantPhoneNumber = bill.MerchantPhoneNumber,
                          MerchantBussinessName = bill.MerchantBussinessName,
                       //  Goods  = bill.Goods, 
                      //   GoodViewModels = bill.GoodViewModels,

                          BillAmount = bill.BillAmount,
                          


                          CustomerNameForBill = bill.CustomerNameForBill,
                          CustomerAcctNumForBill = bill.CustomerAcctNumForBill,

                          CustomerBirth = bill.CustomerBirth,
                          CustomerAddressForBill  = bill.CustomerAddressForBill,
                          CustomerPhoneNumberForBill  = bill.CustomerPhoneNumberForBill,
                         TotalSalaryForBill = bill.TotalSalaryForBill,
                         PayCountForBill  = bill.PayCountForBill,
                         MonthlyPayAmt = bill.MonthlyPayAmt,
                         StartPayDateForBill = bill.StartPayDateForBill,
                         EndPayDateForBill = bill.EndPayDateForBill, 

                         InsurancePay = Math.Round((int.Parse(bill.BillAmount)/1.1) * 0.03).ToString(), 
                         BankProfit = Math.Round(((double.Parse(bill.BillAmount) / 1.1) * 0.1) / int.Parse(bill.PayCountForBill))
                                   .ToString() ,
                         
                         MerchantPay =  Math.Round((double.Parse(bill.BillAmount)/ 1.1) / int.Parse(bill.PayCountForBill))
                                   .ToString(),

                         BillDoc1 = path,

                         Bill1IncertedOn = DateTime.Now, 

                         billStatus = bill.billStatus,  

                         NoteOne = "", 
                         NoteTwo = "", 
                         NoteThree = "", 
                         NoteForRejection = "", 
                         NoteForInsuranceRejection = "", 
                         NoteForOperationRejection = "", 
                         OperationNoteForOperations = "", 
                         DirectiveManagerApprovalNote = "", 
                         DirectiveManagerRejectionNote = "", 
                         FCNotification = true,

                         //addDate 
                         billStatusDate = DateTime.Now,

                       //  GoodLenght = bill.GoodLenght,


                          UserId = bill.UserId,


                    };  

                    // PartialPay payDetails = new PartialPay(); 
                   // List<PartialPay> payDetails = new List<PartialPay>();
                    // payDetails.BillId = bill.BillId;
                  //  for (int i = 0 ; i< int.Parse(bill.PayCountForBill) ; i++) 
                   // { 


                      //  PartialPay pp = new PartialPay();

                      //  List<PartialPay> payDetails = new List<PartialPay>() 

                  //    pp.PartialPayAmount = double.Parse(bill.MonthlyPayAmt);
                   //   pp.Num = i + 1;
                   //   pp.BillSeq = bill.BillSeq; 
                    //  pp.PartialPayDate = DateTime.Now.AddMonths(i); 
                     // pp.PartialPayStatus = "غير مسدد";
                     // payDetails.Add(pp);
                      
                        

                       // payDetails[i].PartialPayAmount = double.Parse(bill.MonthlyPayAmt); 
                       // payDetails[i].BillSeq = bill.BillSeq;
                       // payDetails[i].PartialPayDate = DateTime.Now.AddMonths(i);
                       // payDetails[i].PartialPayStatus = "غير مسدد";
                    //    payDetails.Add(new PartialPay {
                    //        PartialPayAmount = double.Parse(bill.MonthlyPayAmt), 
                    //        BillSeq = bill.BillSeq, 
                    //        PartialPayDate = DateTime.Now.Add
                           

                    //    });

                      //  _db.PartialPays.Add(pp); 
                      //  _db.SaveChanges();

                   // }  
                    

                  //  Console.WriteLine(payDetails.Count);



                  //  List<Good> goods = new List<Good>();


                    //for(int i = 0; i < bill.GoodLenght; i++)
                    //{

                    //    goods.Add(new Good
                    //    { 
                    //        BillId = bill.BillId,  
                    //        Sequence = bill.Goods[i].Sequence,
                    //        GoodName = bill.Goods[i].GoodName, 
                    //        GoodDimensions = bill.Goods[i].GoodDimensions,
                    //        GoodStore = bill.Goods[i].GoodStore, 
                    //        GoodQuentity = bill.Goods[i].GoodQuentity,
                    //        UnitPrice = bill.Goods[i].UnitPrice,
                    //        SumPrice = bill.Goods[i].SumPrice, 
                            

                            

                    //    });
                    //    _db.Goods.Add(goods[i]);

                    //} 



                   

                  //  List<Good> goods = new List<Good>();  

                  //  foreach(var good in goods)

                    //  dataContext.IstmaretTwteens.Add(twteenIstmaraa);
                    //  dataContext.SaveChanges();
                    //dataContext.ImageUplaods.Add(imageUplaod);
                    //dataContext.SaveChanges(); 

                    _db.Bills.Add(bilToSend);
                    //for (var i = 0; i < bilToSend.Goods.Count; i++)
                    //{
                    //    _db.Goods.Add(bilToSend.Goods[i]);
                    //}

                    _db.SaveChanges();



                    return "success saved";
                }
                else
                {
                    return "select File";
                }


            }
            catch (Exception e1)
            {
                return e1.Message;

            }






        } 

        [HttpGet("GetAllBills")] 
        public ActionResult<List<Bill>> getAllBills () 
        {
            List<Bill> bills = _db.Bills.ToList(); 
            

            return bills;
        }  

        [HttpPost("GetBillsBy")] 
        public ActionResult<List<Bill>> GetBillsBy([FromQuery]BillsSearch billsSearch)  
        {
            List<Bill> bills ; 
            bills = _userService.GetAllBillsBy(billsSearch);
            

            return bills;
        } 

        [HttpPost("GetAllBillsMerchant")] 

        public ActionResult<List<Bill>> GetBillsForMerchant(AccountNumber merchantAcctNum)
        {
            List<Bill> billsForMerchant = _userService.GetAllBillsMerchant(merchantAcctNum.AcctNum);

            return billsForMerchant;
        }    

          [HttpPost("RejectedBillsMerchant")] 

        public ActionResult<List<Bill>> GetBillsRejectedForMerchant(AccountNumber merchantAcctNum)
        {
            List<Bill> billsForMerchant = _userService.GetRejectedBillsMerchant(merchantAcctNum.AcctNum);

            return billsForMerchant;
        } 


        

        [HttpGet("GetAllBillsInsurance")]

        public ActionResult<List<Bill>> GetBillsForInsurance() 
        {
            List<Bill> billsForInsurance = _userService.GetAllBillsInsurance(); 
            return billsForInsurance;

        }


        [HttpPost("GetPartialPayByBillSeq")] 

        public ActionResult<List<PartialPay>> GetPartialPayByBillSeq(AccountNumber billSeq)
        {
            List<PartialPay> lstPartialPay = _userService.GetPartialPayByBill(billSeq.AcctNum); 

            return lstPartialPay;
        } 

        [HttpPost("GetBillById")]

        public ActionResult<Bill> GetBillById([FromQuery]NotificationsDetailsAndActions billSeq)
        {
            if(billSeq != null) 
            {
                Bill bill = _userService.GetBillById(billSeq.AcctNum);   
                // CompanyOne
                // CompanyTwo
                // ManagerDirecter
                // Operations

                if(billSeq.RoleUserType == "CompanyOne") {
                bill.FCNotification = false;  
                } 
                 if(billSeq.RoleUserType == "CompanyTwo") {
               bill.SecondCompanyNotification = false;  
                }
                 if(billSeq.RoleUserType == "ManagerDirecter") {
                bill.DirectiveManagerNotification = false;  
                }
                 if(billSeq.RoleUserType == "Operations") {
               bill.OperationsNotification = false;  
                }
                _db.SaveChanges();

                return bill;
            } 
           else {return null;}
        } 

         [HttpPost("GetBillDetails")]

        public ActionResult<Bill> GetBilldetails(AccountNumber billSeq)
        {
            if(billSeq != null) 
            {
               // Bill bill = _userService.GetBillById(billSeq.AcctNum); 
               Bill bill = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.AcctNum); 
               return bill;
            } 
           else {return null;}
        }  

        [HttpGet("GetInsuranceBills")]
        public ActionResult<List<Bill>> GetInsuranceBills()
        {
            
        
                List<Bill> insuredBill = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderBy(b => b.BillDate).ToList(); 

                return insuredBill ;
        
            
        } 

       [HttpGet("GetRejectedInsuranceBills")]
        public ActionResult<List<Bill>> GetRejectedInsuranceBills()
        {
            
        
                List<Bill> uninsuredBills = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == false && b.NoteForInsuranceRejection != null).OrderBy(b => b.BillDate).ToList(); 

                return uninsuredBills ;
        
            
        }

        [HttpPost("InsuranceApproval")]

        public ActionResult<Bill> ApproveInsurance(NoteRejection billSeq)
        {
            if(billSeq.BillSeq != null) 
            {
               // Bill bill = _userService.GetBillById(billSeq.AcctNum); 
               Bill bill = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.BillSeq); 
               bill.IsInsured = true;   
               bill.IsInsuredDate = DateTime.Now;
               bill.NoteTwo = billSeq.NoteForRejection; 
               bill.NoteTwoDate = DateTime.Now; 
               bill.SecondCompanyNotification = true;
            //   Console.OutputEncoding = System.Text.Encoding.UTF8;
             //  Console.WriteLine(3422345.ToWords());
             //  Console.WriteLine(NumberExtensions.ToW(Convert.ToInt32(2323442)));  
            //   string txtWriteamount = "1234";   
             //  Console.WriteLine("zzz");
            //   ToWord toWord = new ToWord(Convert.ToDecimal(txtWriteamount), currencies[Convert.ToInt32(1)]);
            //  string b  = toWord.ConvertToArabic(); 
                

              //  string b = txtWriteamount;

             //  txtWriteamount.Text = ToWord;
             //  Console.WriteLine(b);
               
              // CurrencyInfo.Currencies.UAE
            

            //   Console.WriteLine(DigitsToWordsConverter.Converter("123432"));
               _db.SaveChanges();
               return bill;
            } 
           else {return null;}
        } 



         [HttpPost("RejectInsurance")]

        public ActionResult<Bill> RejectInsurance(NoteRejection billSeq)
        {
            if(billSeq.BillSeq != null) 
            {
               // Bill bill = _userService.GetBillById(billSeq.AcctNum); 
               Bill bill = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.BillSeq); 
               bill.IsInsured = false;  
               bill.IsInsuredDate = DateTime.Now;
               bill.NoteForInsuranceRejection = billSeq.NoteForRejection ; 
               bill.NoteForInsuranceRejectionDate = DateTime.Now;
              // bill.NoteTwo = billSeq.NoteForRejection;
              // Console.OutputEncoding = System.Text.Encoding.UTF8;
             //  Console.WriteLine(3422345.ToWords());
             //  Console.WriteLine(NumberExtensions.ToW(Convert.ToInt32(2323442)));  
             //  string txtWriteamount = "1234";   
             //  Console.WriteLine("zzz");
              // ToWord toWord = new ToWord(Convert.ToDecimal(txtWriteamount), currencies[Convert.ToInt32(1)]);
             // string b  = toWord.ConvertToArabic(); 
                

              //  string b = txtWriteamount;

             //  txtWriteamount.Text = ToWord;
             //  Console.WriteLine(b);
               
              // CurrencyInfo.Currencies.UAE
            

            //   Console.WriteLine(DigitsToWordsConverter.Converter("123432"));
               _db.SaveChanges();
               return bill;
            } 
           else {return null;}
        }  
  


        [HttpPost("billCancelation")] 
        public ActionResult<bool> CancelBill(AccountNumber billSeq) 
        {
            if(billSeq != null) 
            {
                Bill billToCancel = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.AcctNum && b.BillDate > DateTime.Now.AddDays(2) && b.MerchantAcctNumForBill == billSeq.AcctNum.Substring(0, 5)); 
                if(billToCancel != null) {
                billToCancel.billStatus = "ملغاة";  
                billToCancel.billStatusDate = DateTime.Now;
                _db.SaveChanges(); 
                return true;
                }
                else { return false;}

            } 
            else {
                return false;
            }

        } 


        [HttpPost("ConvertToWord")]
        public ActionResult<string> ConvertToWords(AccountNumber num)
        {
            if(num.AcctNum != null) 
            {
               // Bill bill = _userService.GetBillById(billSeq.AcctNum); 
             //  Bill bill = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.AcctNum); 
              // bill.IsInsured = true;  
              // Console.OutputEncoding = System.Text.Encoding.UTF8;
             //  Console.WriteLine(3422345.ToWords());
             //  Console.WriteLine(NumberExtensions.ToW(Convert.ToInt32(2323442)));  
               string txtWriteamount = num.AcctNum;   
             //  Console.WriteLine("zzz");
               ToWord toWord = new ToWord(Convert.ToDecimal(txtWriteamount), currencies[Convert.ToInt32(1)]);
              string b  = toWord.ConvertToArabic();  

              return b ;
                

              //  string b = txtWriteamount;

             //  txtWriteamount.Text = ToWord;
              
               
              // CurrencyInfo.Currencies.UAE
            

            //   Console.WriteLine(DigitsToWordsConverter.Converter("123432"));
               
               
            } 
           else {return null;}
        } 


         [HttpPost("attachInsuranceDoc")] 
         public ActionResult<string> AttachInsuranceDoc([FromForm] Bill bill) 
         {

              try
             {
                 var insuranceDoc = HttpContext.Request.Form.Files[0];
            

                 if (insuranceDoc != null)
                 {
                    

                     FileInfo fi = new FileInfo(insuranceDoc.FileName);
                     var insuranceDocName = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fi.Extension;
                     var path = Path.Combine("", env.ContentRootPath + "\\Images\\" + insuranceDocName);
                    using (var stream = new FileStream(path, FileMode.Create))
                     {
                         insuranceDoc.CopyTo(stream);
                     }

                  
                   
                   
                    


                     Bill billInsDoc = _db.Bills.FirstOrDefault( b => b.BillSeq == bill.BillSeq); 
                     billInsDoc.BillDoc2 = path;
                     billInsDoc.Bill2IncertedOn = DateTime.Now; 


                     //Remove notifications Insurance 

                     billInsDoc.InsuranceComapnyNotification = false;

                  
                 

                     _db.SaveChanges();



                     return "success saved";
                 }
                 else
                 {
                     return "select File";
                 }


             }
             catch (Exception e1)
             {
                 return e1.Message;

             }






        } 

        [HttpGet("OperationsGetBill")] 

        public ActionResult<List<Bill>> GetOperationsBills() 
        {
            List<Bill> operationsBills = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true 
                                                         && b.SecondAuditFromCompanyEmployee == true).OrderBy(b => b.BillDate).ToList();
            
            return operationsBills;

        } 


        [HttpGet("SecondCompanyApprove")] 
        public ActionResult<List<Bill>> GetSecondCompanyApprove() 
        {
            List<Bill> secondCompBills = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true 
                                                        ).OrderBy(b => b.BillDate).ToList();
            
            return secondCompBills;

        } 

        [HttpPost("getGoodsByBillId")]

        public ActionResult<List<Good>> GetGoodsByBillId(AccountNumber billId)
        {
            List<Good> goods = _db.Goods.Where(g => g.BillId == billId.AcctNum).ToList(); 

            return goods; 
        }   

        // [HttpPost("getGoodDetalis")]

        // public ActionResult<List<Good>> GetGoodDetails( GoodId goodId)
        // {
        //     List<Good> goods = _db.Goods.Where(g => g.BillId == goodId.BillId && g.GoodId == goodId.Id).ToList(); 

        //     return goods; 
        // }   

        [HttpPost("getBillGood")]

        public ActionResult<List<Good>> getGoods (AccountNumber billId) 
        {
            List<Good> goods = _db.Goods.Where(g => g.BillId == billId.AcctNum).ToList();
            return goods;
        }


        [HttpPut("EditBillItems/{billSeq}")]

        public ActionResult<Bill> EditBillItems([FromRoute] string billSeq,[FromBody] Bill editedBill) 
        {
            Bill billToUpdate = _db.Bills.Find(billSeq);

            if(billToUpdate != null) 
            {
                billToUpdate.MonthlyPayAmt = editedBill.MonthlyPayAmt; 
                billToUpdate.PayCountForBill = editedBill.PayCountForBill;  

                _db.Bills.Update(billToUpdate); 
                _db.SaveChanges();

                return billToUpdate; 
            } 

            else {
                return null;
            }

        } 


        [HttpPut("EditRejectedBill/{billId}")] 

        public ActionResult<string> UpdateRejectedBill([FromForm]Bill bill, [FromRoute]string billId)
        { 
              try
            {
                var billOne = HttpContext.Request.Form.Files[0];
            

                if (billOne != null)
                {
                    

                    FileInfo fi = new FileInfo(billOne.FileName);
                    var billOneName = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fi.Extension;
                    var path = Path.Combine("", env.ContentRootPath + "\\Images\\" + billOneName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        billOne.CopyTo(stream);
                    } 


                    Bill billToSend = _db.Bills.FirstOrDefault(b => b.BillSeq == billId);  

                    Console.WriteLine("a");
                    
                    
                    billToSend.BillAmount = bill.BillAmount; 
                     Console.WriteLine("a2");

                    billToSend.MonthlyPayAmt = bill.MonthlyPayAmt; 

                     Console.WriteLine("a2");

                    billToSend.BillDoc1 = path; 
                     Console.WriteLine("a3");
                    billToSend.Bill1IncertedOn = DateTime.Now; 
                     Console.WriteLine("a4");
                    billToSend.NoteThree = bill.NoteThree; 
                     Console.WriteLine("a5");
                    billToSend.NoteThreeDate = DateTime.Now;
                     Console.WriteLine("a6");
                    



                
                  //  _db.Bills.Add(billToSend);
               

                    _db.SaveChanges();



                    return "success saved";
                }
                else
                {
                    return "select File";
                }


            }
            catch (Exception e1)
            {
                return e1.Message;

            }







        } 


        //add three text files to the bill for bank core operations 

        [HttpPost("AddTxtFiles")]
        public ActionResult<bool> AddTextFilesToBill(AccountNumber billSeq)
        {
           //  string path= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 

          //   using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "myfile.txt")))
       // {
            //    outputFile.WriteLine("Hello world!");
        //} 
        string doc1 = "payment to  insurance company.txt"; 
        string doc2 = "payment to the profit.txt"; 
        string doc3 = "payment to vendors.txt";

        var path =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc1);
        using StreamWriter writer = new StreamWriter (path, true); 

        var path2 = Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
        using StreamWriter writer2 = new StreamWriter (path2, true); 

        var path3 = Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3);
        using StreamWriter writer3 = new StreamWriter (path3, true);




        DateTime dt = DateTime.Now;
        string dt2String = dt.ToString("dd/MM/yyyy"); 
        dt2String = dt2String.Replace("/", "");
        string braCode = "671";

      //  dt2String = dt2String.Replace(dt2String, string.Empty);
     // string acctNum = "23901";
    //  string insuranceProfit = "300000";
       // Console.WriteLine(dt2String); 

       Bill billToWriteTxtInstructions = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq.AcctNum ); 
       string customerAcctNum = billToWriteTxtInstructions.CustomerAcctNumForBill ; 
       string insuranceProfit = Math.Round((int.Parse(billToWriteTxtInstructions.BillAmount)/1.1) * 0.03).ToString(); 
       string bankProfitMonthly = Math.Round(((double.Parse(billToWriteTxtInstructions.BillAmount) / 1.1) * 0.1) / int.Parse(billToWriteTxtInstructions.PayCountForBill))
                                   .ToString();
       string merchantRevenue = Math.Round((double.Parse(billToWriteTxtInstructions.BillAmount)/ 1.1) / int.Parse(billToWriteTxtInstructions.PayCountForBill))
                                 .ToString();

       
    
       writer.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,42,{insuranceProfit},payment to insurance");
       writer.Close(); 
       writer2.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,40,{bankProfitMonthly},payment to profit");
       writer2.Close();  
       writer3.WriteLine($"H,{dt2String},2\nD,0{braCode}00{customerAcctNum}0012517000,41,{merchantRevenue},payment to the vendor"); 
       writer3.Close();


        return true;

        }

        [HttpPut("RestEditedBill/{BillId}")]

        public ActionResult<Bill> ResetEditedBill([FromRoute]string billId) 
        {
            Bill billToReset = _db.Bills.FirstOrDefault(b => b.BillSeq == billId); 

            billToReset.FirstAuditFromCompanyEmployee = false;
            billToReset.SecondAuditFromCompanyEmployee = false; 
            billToReset.IsInsured = false; 

            _db.SaveChanges(); 

            return billToReset;


        } 





        //additional code 

        [HttpGet("DownloadBillDoc")] 
        
        public async Task<ActionResult> GetBillDoc([FromQuery] string billId)
        {  

            Bill billToGetTheDoc = _db.Bills.FirstOrDefault(b => b.BillSeq == billId); 

            Console.WriteLine(billToGetTheDoc.BillDoc1);

            var fileName = billToGetTheDoc.BillDoc1.ToString() ;

          //   string doc1 = "payment to  insurance company.txt"; 
          //    string doc2 = "payment to the profit.txt"; 
             // string doc3 = "payment to vendors.txt";

            var path =  Path.Combine(fileName);  
           // var path2 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
           // var path3 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3); 
           // var fileName = "Operations.zip";
        
          

            
            var memory = new MemoryStream();  
           

            using (var stream = new FileStream(path, FileMode.Open))
            { 
                           

                await stream.CopyToAsync(memory);
            }  
                     

            memory.Position = 0;  
                     

            var ext = Path.GetExtension(path).ToLowerInvariant();  
                     

            return File(memory, GetTypes()[ext], Path.GetFileName(path));  
                     // Console.WriteLine("eee"); 

            
            
        } 


        [HttpGet("DownloadInsurce")] 
        
        public async Task<ActionResult> GetInsuraceDocs([FromQuery] string billId)
        {  

            Bill billToGetTheDoc = _db.Bills.FirstOrDefault(b => b.BillSeq == billId); 

           // Console.WriteLine(billToGetTheDoc.BillDoc1);

            var fileName = billToGetTheDoc.BillDoc2.ToString() ;

          //   string doc1 = "payment to  insurance company.txt"; 
          //    string doc2 = "payment to the profit.txt"; 
             // string doc3 = "payment to vendors.txt";

            var path =  Path.Combine(fileName);  
           // var path2 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
           // var path3 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3); 
           // var fileName = "Operations.zip";
         
          

            
            var memory = new MemoryStream();  
           

            using (var stream = new FileStream(path, FileMode.Open))
            { 
                        
                await stream.CopyToAsync(memory);
            }  
                     

            memory.Position = 0;  
                     

            var ext = Path.GetExtension(path).ToLowerInvariant();  
                    

            return File(memory, GetTypes()[ext], Path.GetFileName(path));  
                     // Console.WriteLine("eee"); 

            
            
        }

        [HttpGet("DownloadInsurancePaymant")]
        public async Task<ActionResult> Download() 
        {  
              string doc1 = "payment to  insurance company.txt"; 
          //    string doc2 = "payment to the profit.txt"; 
             // string doc3 = "payment to vendors.txt";

            var path =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc1);  
           // var path2 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
           // var path3 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3); 
           // var fileName = "Operations.zip";


            
            var memory = new MemoryStream(); 
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            } 
            memory.Position = 0; 
            var ext = Path.GetExtension(path).ToLowerInvariant(); 
            return File(memory, GetTypes()[ext], Path.GetFileName(path)); 
            
            
        }  

          [HttpGet("DownloadBankProfit")]
        public async Task<ActionResult> Download2() 
        {  
            //  string doc1 = "payment to  insurance company.txt"; 
              string doc2 = "payment to the profit.txt"; 
             // string doc3 = "payment to vendors.txt";

           // var path =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc1);  
            var path2 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
           // var path3 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3); 
           // var fileName = "Operations.zip";


            
            var memory = new MemoryStream(); 
            using (var stream = new FileStream(path2, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            } 
            memory.Position = 0; 
            var ext = Path.GetExtension(path2).ToLowerInvariant(); 
            return File(memory, GetTypes()[ext], Path.GetFileName(path2)); 
            
            
        } 

          [HttpGet("DownloadVenderPayment")]
        public async Task<ActionResult> Download3() 
        {  
              //string doc1 = "payment to  insurance company.txt"; 
          //    string doc2 = "payment to the profit.txt"; 
              string doc3 = "payment to vendors.txt";

           // var path =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc1);  
           // var path2 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc2); 
            var path3 =  Path.Combine("", env.ContentRootPath + "\\TextDocs\\" + doc3); 
           // var fileName = "Operations.zip";


            
            var memory = new MemoryStream(); 
            using (var stream = new FileStream(path3, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            } 
            memory.Position = 0; 
            var ext = Path.GetExtension(path3).ToLowerInvariant(); 
            return File(memory, GetTypes()[ext], Path.GetFileName(path3)); 
            
            
        }

        // [HttpGet("DownloadTxT2")]  

        // public FileResult Get


        private Dictionary<string,string> GetTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},  
                {".pdf", "application/pdf"}, 
                {".png", "image/png"},
                {".jpg", "image/jpg"}

            };

        }


        

        


        // [HttpPost("GetHistory")] 
        // public ActionResult<HistoryOfBill> GetBillHistory([FromQuery]AccountNumber billSeq) 
        // {
    
        // }


        



    

       // InsuranceApproval

        
        

      


    }
}