using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructures.Data.EntityMappings.Default;

namespace Store.Infrastructures.Data.Contexts
{
    public class DefaultDbContext : DbContext, IDefaultDbContext
    {
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected DefaultDbContext()
        {
            Database.Migrate();
        }

        public DefaultDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SampleMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new OrderedProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
