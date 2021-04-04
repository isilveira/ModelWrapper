using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Images
{
    public class DeleteImageService : DomainService<Image>,IDeleteImageService
    {
        private IDefaultDbContext Context { get; set; }
        public DeleteImageService(
            IDefaultDbContext context,
            ImageValidator entityValidator,
            DeleteImageSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Image entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            Context.Images.Remove(entity);

            return Task.CompletedTask;
        }
    }
}
