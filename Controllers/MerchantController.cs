using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private IUserService _userService;

        public MerchantController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<List<User>>> GetAllMerchants ()
        {
            return await _userService.GetAllAdmins();

        }

        // [HttpPut("Update/{id}")]
        // public async Task<IActionResult> UpdateMerchant([FromBody] MerchantRegisterViewModel model, [FromRoute]string id )
        // {
        //     await _userService.EditUserInfo(id, model);

        //     return Ok("Success Update User");


        // }

        [HttpDelete("Delete/{id}")]

        public async Task<IActionResult> DeleteMerchant([FromRoute] string id)
        {
            await _userService.DeleteUser(id);
            return Ok("User Succesfully deleted");
        }

        

    }
}