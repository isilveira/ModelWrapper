using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Customers
{
    public class PatchCustomerService : DomainService<Customer>, IPatchCustomerService
    {
        private IDefaultDbContext Context { get; set; }
        public PatchCustomerService(
            IDefaultDbContext context,
            CustomerValidator entityValidator,
            PatchCustomerSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Customer entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            return Task.CompletedTask;
        }
    }
}
