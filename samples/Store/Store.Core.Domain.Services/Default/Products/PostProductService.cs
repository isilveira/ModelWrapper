using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Products
{
    public class PostProductService : DomainService<Product>, IPostProductService
    {
        private IDefaultDbContext Context { get; set; }
        public PostProductService(
            IDefaultDbContext context,
            ProductValidator entityValidator,
            PostProductSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(Product entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.Products.AddAsync(entity);
        }
    }
}
