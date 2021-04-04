using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Categories
{
    public class DeleteCategoryService : DomainService<Category>,IDeleteCategoryService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteCategoryService(
            IDefaultDbContext context,
            CategoryValidator entityValidator,
            DeleteCategorySpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Category entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.Categories.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
