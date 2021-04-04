using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Orders
{
    public class DeleteOrderService : DomainService<Order>,IDeleteOrderService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteOrderService(
            IDefaultDbContext context,
            OrderValidator entityValidator,
            DeleteOrderSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Order entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.Orders.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
