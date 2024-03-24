using APIServices.Entities;
using APIServices.Entities.Contacts;
using APIServices.Entities.Purchase;
using APIServices.Entities.Stock;
using Microsoft.EntityFrameworkCore;

namespace APIServices.Data
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
