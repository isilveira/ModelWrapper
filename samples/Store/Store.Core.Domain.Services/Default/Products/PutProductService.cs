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
    public class PutProductService : DomainService<Product>, IPutProductService
    {
        private IDefaultDbContext Context { get; set; }
        public PutProductService(
            IDefaultDbContext context,
            ProductValidator entityValidator,
            PutProductSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Product entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            return Task.CompletedTask;
        }
    }
}
