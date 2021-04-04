using Store.Core.Domain.Entities.Default;
using Microsoft.EntityFrameworkCore;

namespace Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts
{
    public interface IDefaultDbContextQuery
    {
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
