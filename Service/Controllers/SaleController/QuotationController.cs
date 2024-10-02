using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Controllers;
using Sonaar.Domain.DataContext;
using Sonaar.Domain.Dto.ReportGeneration;
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
                    .ToListAsync();
                result.Data = quotationList;
            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.Data = new List<Quotation>();
            }
            return result;
        }
    }
}

