using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.OrderedProducts
{
    public class PostOrderedProductService : DomainService<OrderedProduct>, IPostOrderedProductService
    {
        private IDefaultDbContext Context { get; set; }
        public PostOrderedProductService(
            IDefaultDbContext context,
            OrderedProductValidator entityValidator,
            PostOrderedProductSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(OrderedProduct entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.OrderedProducts.AddAsync(entity);
        }
    }
}
