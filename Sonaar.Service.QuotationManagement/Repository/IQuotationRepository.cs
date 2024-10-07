using System;
using Sonaar.Domain.Entities.Quotations;

namespace Sonaar.Service.QuotationManagement.Repository
{
	public interface IQuotationRepository
	{
        Task<List<Quotation>> GetAllQuotationsAsync();
    }
}

