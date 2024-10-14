using System;
using Sonaar.Domain.Entities.Quotations;
using Sonaar.Service.QuotationManagement.Repository;

namespace Sonaar.Service.QuotationManagement.Service
{
	public class QuotationService
	{
        private readonly IQuotationRepository _quotationRepository;

        public QuotationService(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }

        public async Task<List<Quotation>> GetAllQuotationsAsync()
        {
            return await _quotationRepository.GetAllQuotationsAsync();
        }
    }
}

