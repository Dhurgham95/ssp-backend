using Backend.Models;
using Backend.PartialModels;
using Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<UserLoginResponse> LoginSuperAdminAsync(SuperAdminLoginViewModel model);
        Task<UserMangageResponse> RegisterSuperAdminAsync(SuperAdminRegisterViewModel model);

        Task<UserMangageResponse> RegisterAdminAsyc(AdminRegisterViewModel model);

        Task<List<User>> GetAllAdmins();
        Task<User> EditUserInfo(string userId, AdminRegisterViewModel usrUpdate);

        Task<string> DeleteUser(string userId);

        Task ResetPassword(string userId, string newPasswd);

      //  Task <UserMangageResponse>LoginAdminAsync(AdminLoginViewModel model);
        Task<UserMangageResponse> LoginAdminAsync(AdminLoginViewModel model);

        Task<UserMangageResponse> RegisterMerchantAsync(MerchantRegisterViewModel model);

        Task<UserLoginResponse> LoginMechantAsync(MerchantLoginViewModel model);

        Task<AskBank> SendAskAccountStatus(AskBank askBank);

        Task<AskBank> CheckAccountNumberExistsAndRetriveRelatedData(string acctNum); 
        Task<UserMangageResponse> RegisterBranchEmployeeRespondent (BranchEmployeeRespondentViewModel model);

        Task<UserLoginResponse> LoginBranchEmployeeRespondent (BranchEmployeeRespondentLoginViewModel model);  

        
        Task<UserMangageResponse> RegisterInsuranceCompany (InsuranceCompany model);

        Task<UserLoginResponse> LoginInsuracneCompany (InsuranceCompanyLogin model);   


        IList<AskBank> GetAllRequestInfo (); 

        Task<AskBank> SendAskBankAnswer (long askBankId, AskBank askBank); 

        IList<AskBank> GetAskBanksByUserId(string userId); 

        PersonalAccount CheckIfCustomerIsAllowToTakeMurabaha(string acctNum);  

        PersonalAccount CheckIfCustomerIsAllowToTakeMurabaha2(string acctNum); 

        Test GetPayInfo(InputTest npt);  

         Test GetPayInfo2(InputTest npt); 
        //  Test GetPayInfo3(InputTest npt); 

        // OutputValues GetPayInfo3(InputValues inputValues); 
        
        bool AccountNumberOperations(string accNum);
        PersonalAccount VerificationAndPhoneNumberOperations(string code);

        OutputValues GetPayInfo3(InputValues inputValues);

        Task<UserMangageResponse> RegisterOperationsEmployee (BranchEmployeeRespondentViewModel model);

        Task<UserLoginResponse> LoginOperationsEmployee (BranchEmployeeRespondentLoginViewModel model);  

        Bill SendBillDataToServer(Bill bill); 

        Good AddGood(Good good);

        Good EditGood(string goodId,Good good);  

        Good MainGoodEdit(string goodId,Good good); 
        string DeleteGood(string goodId);  

        List<Good> GetAllGoods(string BillId); 

        double CaltotalSum(string billId); 

        List<Bill> GetAllBillsBy(BillsSearch billsSearch); 

       // List<PartialPay> GetPartialPayByBill(Bill bill); 

      List<Bill> GetAllBillsMerchant(string MerchantAcctNum);  
      List<Bill> GetAllBillsInsurance();

       List<PartialPay> GetPartialPayByBill (string billSeq); 

       Bill GetBillById (string billSeq);  

       List<Bill> GetRejectedBillsMerchant(string MerchantAcctNum); 

       List<Bill> GetAllBillsInsSuc (); 

      

     



    }
}
