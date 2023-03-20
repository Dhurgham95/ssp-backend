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

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IUserService _userService;
        public TestController(DataContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        } 

        [HttpPost("otp")]
        public ActionResult<bool> OtpSendTest(AccountNumber acctNum)
        {
            var result = _userService.AccountNumberOperations(acctNum.AcctNum);
            return result;
        }

        [HttpPost("codeOtp")]
        public ActionResult<PersonalAccount> OtpCheckTest(AccountNumber code)
        {
            PersonalAccount result = _userService.VerificationAndPhoneNumberOperations(code.AcctNum);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalAccount>>> getAccount()
        {
          //  List<PersonalAccount> accts = new < PersonalAccount > ();
            return await _db.PersonalAccounts.ToListAsync();


        } 
        //[HttpPost("aaa")]
        //public async Task<ActionResult<PersonalAccount>> getDdd(string)
        //{

        //}
    }
}