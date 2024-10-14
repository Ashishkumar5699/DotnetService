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
        private readonly QuotationService _quotationService;

        public QuotationController(DataContext context, ITokenService tokenService, IMapper mapper,QuotationService quotationService) : base(context, tokenService)
        {
            _mapper = mapper;
            _quotationService = quotationService;
        }


        [HttpPost("CreateQuotation")]
        public async Task<ExecResult> CreateQuote(PrintBillDto _createQuoteDTO)
        {
            var result = new ExecResult();

            try
            {
                var _quotation = _mapper.Map<Quotation>(_createQuoteDTO);
                
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
                var quotationList = await _quotationService.GetAllQuotationsAsync();
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

