using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Controllers;
using Sonaar.Domain.DataContexts;
using Sonaar.Domain.Dto.ReportGeneration;
using Sonaar.Domain.Entities.Product;
using Sonaar.Domain.Entities.Quotations;
using Sonaar.Domain.Response;
using Sonaar.Interface;
using Sonaar.Service.QuotationManagement.Service;

namespace Sonaar.Service.APi.Controllers.SaleController
{
    public class QuotationController : BaseApiController
    {
        private readonly IMapper _mapper;
        public QuotationController(DataContext context, ITokenService tokenService, IMapper mapper) : base(context, tokenService)
        {
            _mapper = mapper;
        }

        private readonly QuotationService quotationService;

        [HttpPost("CreateQuotation")]
        public async Task<ExecResult> CreateQuote(PrintBillDto _createQuoteDTO)
        {
            var result = new ExecResult();

            try
            {
                var _quotation = _mapper.Map<Quotation>(_createQuoteDTO);
                // add product in product table the merge it in Key
                //add gst amount in table and merge it with key
                //tranjection commit rolback
                await _context.Quotations.AddAsync(_quotation);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpGet("GetAllQuotation")]
        public async Task<ResponseResult<List<Quotation>>> GetQuotationsAsync()
        {
            var result = new ResponseResult<List<Quotation>>();
            try
            {
                var quotationList = await _context.Quotations
                    .Include(q => q.ContactDetails) // Include related entities as needed
                    .Include(q => q.ProductList)     // Include products
                    .Include(q => q.GSTAmount)       // Include GST amount
                    //.Select(q => new Quotation
                    //{
                    //    QuotationId = q.QuotationId,
                    //    //QuotationNumber = q.QuotationNumber,
                    //    GSTAmount = q.GSTAmount, // Adjust as needed

                    //    ProductList = q.ProductList.Select(p => new ProductEntity
                    //    {
                    //        ProductId = p.ProductId,
                    //        QuotationId = p.QuotationId // Include if necessary
                    //                                    // Map other necessary fields
                    //    }).ToList()
                    //})
                    .ToListAsync();
                result.Data = quotationList;
            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.Message = ex.Message;
                result.Data = new List<Quotation>();
            }
            return result;
        }
    }
}

