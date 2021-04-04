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
    public class PutImageService : DomainService<Image>, IPutImageService
    {
        private IDefaultDbContext Context { get; set; }
        public PutImageService(
            IDefaultDbContext context,
            ImageValidator entityValidator,
            PutImageSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Image entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            return Task.CompletedTask;
        }
    }
}
