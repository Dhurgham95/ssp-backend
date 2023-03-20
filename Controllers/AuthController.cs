using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.PartialModels;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserService _userService; 
        private readonly DataContext _db;

        public AuthController(IUserService userService , DataContext db)
        {
            _userService = userService; 
            _db = db;

        }

        [HttpPost("SuperAdminLogin")]
        public async Task<IActionResult> SuperAdminLoginAsyncM([FromBody]SuperAdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginSuperAdminAsync(model);

                if (result.IsSuccess)
                {

                    return Ok(result);
                }
                return BadRequest("not valid user info");

            }

            return BadRequest("not valid Login info");
        }


      
        [HttpPost("SuperAdminRegister")]
        public async Task<IActionResult> SuperAdminRegisterAsync([FromBody]SuperAdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterSuperAdminAsync(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }


        [HttpPost("AdminRegister")]
        public async Task<IActionResult> AdminRegisterAsync([FromBody]AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAdminAsyc(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        } 

        [HttpPost("AdminLogin")]  

          public async Task<IActionResult> AdminLogin([FromBody]AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAdminAsync(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }  

        


        [HttpPost("MerchantRegister")]
        public async Task<IActionResult> MerchnatRegisterAsync([FromBody]MerchantRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterMerchantAsync(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }

        [HttpPost("MerchantLogin")]
        public async Task<IActionResult> MerchnatLoginAsync([FromBody]MerchantLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginMechantAsync(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        } 

        [HttpPost("BranchEmployeeRespondentRegister")]
        public async Task<IActionResult> BranchEmployeeRegister([FromBody]BranchEmployeeRespondentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterBranchEmployeeRespondent(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        } 

        [HttpPost("BranchEmployeeRespondentLogin")]
        public async Task<IActionResult> BranchEmployeeLogin([FromBody]BranchEmployeeRespondentLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginBranchEmployeeRespondent(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }  

          [HttpPost("OperationsRegister")]
        public async Task<IActionResult> OperationsRegister([FromBody]BranchEmployeeRespondentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterBranchEmployeeRespondent(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }  

        [HttpPost("OperationsLogin")]
        public async Task<IActionResult> OperationsLogin([FromBody]BranchEmployeeRespondentLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginBranchEmployeeRespondent(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }   

          [HttpPost("InsuranceRegister")]
        public async Task<IActionResult> RegisterInsurance([FromBody]InsuranceCompany model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterInsuranceCompany(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }  

        [HttpPost("InsuranceLogin")]
        public async Task<IActionResult> LoginInsurance([FromBody]InsuranceCompanyLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginInsuracneCompany(model);

                if (result.IsSuccess)
                {
                    //to do dd confirm email
                    return Ok(result);
                }

                return BadRequest(result);

            }

            return BadRequest("some parameters are not valid");


        }  

        [HttpPost("CheckUser")] 

        public ActionResult<String> CheckUSerRole(AccountNumber phoneNum) 
        { 
             User user = _db.Users.FirstOrDefault( u => u.PhoneNumber == phoneNum.AcctNum); 

            if(user != null)
            {
                return user.Role;
            } 
            else {
                return null;
            }
            
        } 

        [HttpPost("CheckAdmin")] 

        public ActionResult<String> CheckAdminRole(AccountNumber phoneNum) 
        { 
             User user = _db.Users.FirstOrDefault( u => u.UserName == phoneNum.AcctNum); 

            if(user != null)
            {
                return user.Role;
            } 
            else {
                return null;
            }
            
        } 
       
    }
}
