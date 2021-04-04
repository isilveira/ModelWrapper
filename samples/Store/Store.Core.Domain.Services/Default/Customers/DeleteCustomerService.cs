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
    public class DeleteCustomerService : DomainService<Customer>,IDeleteCustomerService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteCustomerService(
            IDefaultDbContext context,
            CustomerValidator entityValidator,
            DeleteCustomerSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Customer entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.Customers.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
