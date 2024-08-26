using Sonaar.Entities;
using Sonaar.Entities.Purchase;
using Sonaar.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.Entities.Contacts;

namespace Sonaar.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Gold> GoldStock { get; set; }

        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }

        public DbSet<ContactDetails> ContactDetails { get; set; }
    }
}
