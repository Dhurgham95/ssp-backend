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

namespace Backend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
    [ApiController]
    public class AskBankController : ControllerBase
    {
        private IUserService _userService; 
        private DataContext _db;

        public AskBankController(IUserService userService, DataContext db)
        {
            _userService = userService;
            _db = db;

        }

        
      

        [HttpPost("sendAskRequest")]
        public async Task<IActionResult> SendAskReqest([FromBody]AskBank ask) 
        {
            var askResult = await _userService.SendAskAccountStatus(ask);

            return Ok(askResult);
        } 

        [HttpGet("GetAllAB")] 

        public  IList<AskBank> GetAllRequestForAskBank ()
        {
            //string userId = "ee396f17-5b75-447a-9f4a-b4370a7fecf9";
            var result =  _userService.GetAllRequestInfo(); 

          //  IList<AskBank> res = result;

            return result;

            
        } 

        [HttpPut("SendAnswer/{id}")]
        public async Task<IActionResult> SendAnswer([FromBody] AskBank askBank, [FromRoute]long id )
        {
            await _userService.SendAskBankAnswer(id,askBank);

            return Ok("Success Send Answer");


        } 

        [HttpPost("GetAskByUserId")]
        public IList<AskBank> GetAllRequestByUseId(IdU idd)
        {
          //  string userId = "8c734772-48af-459b-9adb-b6a5125e7b97";
            var result = _userService.GetAskBanksByUserId(idd.UserId);

            //  IList<AskBank> res = result;

            return result;


        } 

        [HttpPost("SendCNumAndNext")]
        public PersonalAccount GetDataFromAcctNum(AccountNumber num)
        {
            PersonalAccount result = new PersonalAccount();
            result = _userService.CheckIfCustomerIsAllowToTakeMurabaha(num.AcctNum);

            return result;
            
        }   

        [HttpPost("SendCNumAndNxt")]
        public PersonalAccount GetDataFromAcctNumber(AccountNumber num)
        {
            PersonalAccount result = new PersonalAccount();
            result = _userService.CheckIfCustomerIsAllowToTakeMurabaha2(num.AcctNum);

            return result;
            
        }  

        

        [HttpPost("Pay")]
        public Test Operation (InputTest npt) 
        { 
            Test result = new Test(); 

            result = _userService.GetPayInfo(npt);

            return result;
            
        }  

             [HttpPost("Pay2")]
        public Test Operation2 (InputTest npt) 
        { 
            Test result = new Test(); 

            result = _userService.GetPayInfo2(npt);

            return result;
            
        }  

       [HttpPost("Pay3")]
        public OutputValues Operation3(InputValues inputValues)
        {
            OutputValues result = new OutputValues();

            result = _userService.GetPayInfo3(inputValues);


            return result;

        }

        //  [HttpPost("Pay4")]
        // public OutputValues Operation4 (InputValues npt) 
        // { 
        //     OutputValues result = new OutputValues(); 

        //     result = _userService.GetPayInfo4(npt);

        //     return result;
            
        // } 

        [HttpPut("seedPersonal/{acctNum}")] 
        public PersonalAccount Seed ([FromBody]PersonalAccount personalAccount , [FromRoute]string acctNum ) 
        {
            var personalacct = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == acctNum) ;  


            
            if (personalacct != null)  
            { 
                personalacct.AllowedToTakeTmweel = personalAccount.AllowedToTakeTmweel;
                personalacct.NoteForGivenTmweel = personalAccount.NoteForGivenTmweel;
              
                _db.SaveChanges();

                return personalacct;


            }  

            return null;


        }


        // [HttpGet("GetAllAsk")]

        // public async Task<IActionResult> GetAskReguest()

     

    }
}