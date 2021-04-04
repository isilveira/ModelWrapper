using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Samples;
using Store.Core.Domain.Validations.DomainValidations.Default.Samples;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Samples
{
    public class PatchSampleService : DomainService<Sample>, IPatchSampleService
    {
        private IDefaultDbContext Context { get; set; }
        public PatchSampleService(
            IDefaultDbContext context,
            SampleValidator entityValidator,
            PatchSampleSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }

        public override Task Run(Sample entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            return Task.CompletedTask;
        }
    }
}
