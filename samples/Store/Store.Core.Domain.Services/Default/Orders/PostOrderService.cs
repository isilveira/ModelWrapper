using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Orders
{
    public class PostOrderService : DomainService<Order>, IPostOrderService
    {
        private IDefaultDbContext Context { get; set; }
        public PostOrderService(
            IDefaultDbContext context,
            OrderValidator entityValidator,
            PostOrderSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(Order entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.Orders.AddAsync(entity);
        }
    }
}
