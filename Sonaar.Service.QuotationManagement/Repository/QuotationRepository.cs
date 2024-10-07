using System;
using Sonaar.Domain.Entities.Quotations;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.DataContexts;

namespace Sonaar.Service.QuotationManagement.Repository
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly DataContext _dataContext;

        public QuotationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Quotation>> GetAllQuotationsAsync()
        {
            return await _dataContext.Quotations
                .Include(q => q.ContactDetails) // Include related entities as needed
                .Include(q => q.ProductList)     // Include products
                .Include(q => q.GSTAmount)       // Include GST amount
                .ToListAsync();
        }
    }
}

