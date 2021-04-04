using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Customers
{
    public class PostCustomerService : DomainService<Customer>, IPostCustomerService
    {
        private IDefaultDbContext Context { get; set; }
        public PostCustomerService(
            IDefaultDbContext context,
            CustomerValidator entityValidator,
            PostCustomerSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(Customer entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.Customers.AddAsync(entity);
        }
    }
}
