using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Products
{
    public class DeleteProductService : DomainService<Product>,IDeleteProductService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteProductService(
            IDefaultDbContext context,
            ProductValidator entityValidator,
            DeleteProductSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Product entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.Products.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
