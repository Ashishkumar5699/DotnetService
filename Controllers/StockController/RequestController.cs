using APIServices.Data;
using APIServices.DTOs.PurchaseDto;
using APIServices.Entities.Purchase;
using APIServices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIServices.Controllers.StockController
{
    public class RequestController : BaseApiController
    {
        public RequestController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpPost("AddGoldPurchaseRequst")]
        public async Task<ActionResult<GoldPurchaseDTo>> AddGoldPurchaseRequst(PurchaseRequest goldPurchase)
        {
            goldPurchase.IsApproved = null;
            _context.PurchaseRequests.Add(goldPurchase);
            await _context.SaveChangesAsync();

            return new GoldPurchaseDTo
            {
                PurchaseRequestId = goldPurchase.PurchaseRequestId,
                MatelType = goldPurchase.MatelType,
                ItemType = goldPurchase.ItemType,
                Brand = goldPurchase.Brand,
                ManufactureId = goldPurchase.ManufactureId,
                ManufactureName = goldPurchase.ManufactureName,
                GrossWeight = goldPurchase.GrossWeight,
                LessWeight = goldPurchase.LessWeight,
                NetWeight = goldPurchase.NetWeight,
                Quantity = goldPurchase.Quantity,
                Rate = goldPurchase.Rate,
                Wastage = goldPurchase.Wastage,
                Labour = goldPurchase.Labour,
                PurchaseDate = goldPurchase.PurchaseDate
            };
        }
        
        [HttpPost("DeleteGoldPurchaseRequst/{requestid}")]
        public async Task<ActionResult<GoldPurchaseDTo>> DeleteGoldPurchaseRequst(int requestid)
        {
            var goldPurchaseRequest = await _context.PurchaseRequests.FindAsync(requestid);
            if (goldPurchaseRequest == null)
                return BadRequest("Request not fount");
            _context.PurchaseRequests.Add(goldPurchaseRequest);
            await _context.SaveChangesAsync();

            return new GoldPurchaseDTo
            {
                
                PurchaseRequestId = goldPurchaseRequest.PurchaseRequestId,
                MatelType = goldPurchaseRequest.MatelType,
                ItemType = goldPurchaseRequest.ItemType,
                Brand = goldPurchaseRequest.Brand,
                ManufactureId = goldPurchaseRequest.ManufactureId,
                ManufactureName = goldPurchaseRequest.ManufactureName,
                GrossWeight = goldPurchaseRequest.GrossWeight,
                LessWeight = goldPurchaseRequest.LessWeight,
                NetWeight = goldPurchaseRequest.NetWeight,
                Quantity = goldPurchaseRequest.Quantity,
                Rate = goldPurchaseRequest.Rate,
                Wastage = goldPurchaseRequest.Wastage,
                Labour = goldPurchaseRequest.Labour,
                PurchaseDate = goldPurchaseRequest.PurchaseDate
            };
        }

        [HttpGet("GetGoldRequestDetail/{id}")]
        public async Task<PurchaseRequest> GetGoldRequestDetail(int? id)
        {
            PurchaseRequest purchaseRequest;
            purchaseRequest = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequest != null)
                return purchaseRequest;

            return new PurchaseRequest();
        }
    }
}
