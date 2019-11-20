using Microsoft.EntityFrameworkCore;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using Store.Core.Domain.Entities;

namespace Store.Core.Infrastructures.Data
{
    public class StoreContext : DbContext, IStoreContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        protected StoreContext()
        {
            base.Database.EnsureCreated();
        }

        public StoreContext(DbContextOptions options) : base(options)
        {
            base.Database.EnsureCreated();
        }
    }
}
