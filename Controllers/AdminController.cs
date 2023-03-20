using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.ViewModels;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<List<User>>> GetAllAdmins ()
        {
            return await _userService.GetAllAdmins();

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminRegisterViewModel model, [FromRoute]string id )
        {
            await _userService.EditUserInfo(id, model);

            return Ok("Success Update User");


        }

        [HttpDelete("Delete/{id}")]

        public async Task<IActionResult> DeleteAdmin([FromRoute] string id)
        {
            await _userService.DeleteUser(id);
            return Ok("User Succesfully deleted");
        }


    }
}