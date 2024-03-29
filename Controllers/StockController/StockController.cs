using APIServices.Data;
using APIServices.DTOs.StockDto;
using APIServices.Entities.Stock;
using APIServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIServices.Controllers.StockController
{
    //[Authorize]
    [AllowAnonymous]
    public class StockController : BaseApiController
    {
        public StockController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gold>>> GetAllStock()
        {
            return await _context.GoldStock.ToListAsync();
        }

        [HttpPost]
        public ActionResult<Gold> AddGold(GoldDto goldDto)
        {
            return BadRequest("Api is under construction please try after some time");
        }

    }
}
