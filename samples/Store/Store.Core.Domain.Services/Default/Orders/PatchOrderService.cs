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
    public class PatchOrderService : DomainService<Order>, IPatchOrderService
    {
        private IDefaultDbContext Context { get; set; }
        public PatchOrderService(
            IDefaultDbContext context,
            OrderValidator entityValidator,
            PatchOrderSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Order entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            return Task.CompletedTask;
        }
    }
}
