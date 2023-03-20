using System.Collections.Generic;
using Backend.Models;
using Backend.PartialModels;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController : ControllerBase
    {
        private IUserService _userService; 
        public GoodController(IUserService userService)
        {
            _userService = userService;
        } 

        [HttpPost("Add")] 
        public ActionResult<Good> AddGood (Good good) 
        {
            var result = _userService.AddGood(good); 
            return result;
        }


        [HttpPost("GetAll")]
        public  ActionResult<List<Good>> GetAllGoods (GoodsGet goodsGet)
        {
            var result = _userService.GetAllGoods(goodsGet.billId);

            return result;

        } 

        [HttpPut("UpdateM/{goodId}")]

        public ActionResult<Good> UpdateGoodM([FromBody] Good good, [FromRoute] string goodId)
        {
            var result = _userService.MainGoodEdit(goodId, good);

            return result;
        }


        [HttpPut("Update/{goodId}")]
        public ActionResult<Good> UpdateGood([FromBody] Good good, [FromRoute]string goodId )
        {
            var result = _userService.EditGood(goodId, good); 

            return result;



        }

        [HttpDelete("Delete/{goodId}")]

        public ActionResult<string> DeleteGood([FromRoute] string goodId)
        {
            var result =_userService.DeleteGood(goodId); 

            return result;
            
        } 

         [HttpPost("sum")]

        public ActionResult<double> CalSum(AccountNumber billId)
        {
            var result =_userService.CaltotalSum(billId.AcctNum); 

            return result;
            
        }


        
    }
}