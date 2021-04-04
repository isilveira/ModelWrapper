using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.OrderedProducts
{
    public class DeleteOrderedProductService : DomainService<OrderedProduct>,IDeleteOrderedProductService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteOrderedProductService(
            IDefaultDbContext context,
            OrderedProductValidator entityValidator,
            DeleteOrderedProductSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(OrderedProduct entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.OrderedProducts.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
