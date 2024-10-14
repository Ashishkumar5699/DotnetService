using Sonaar.DTOs.StockDto;
using Sonaar.Entities.Stock;
using Sonaar.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.DataContexts;

namespace Sonaar.Controllers.StockController
{
    //[Authorize]
    [AllowAnonymous]
    public class StockController : BaseApiController
    {
        public StockController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Sonaar.Domain.Models.Products.Gold>>> GetAllStock()
        //{
        //    //return await _context.GoldStock.ToListAsync();
        //    return BadRequest("Api is under construction please try after some time");

        //}

        //[HttpPost]
        //public ActionResult<Gold> AddGold(GoldDto goldDto)
        //{
        //    return BadRequest("Api is under construction please try after some time");
        //}

    }
}
