using BAYSOFT.Abstractions.Core.Domain.Entities;
using BAYSOFT.Abstractions.Core.Domain.Exceptions;
using BAYSOFT.Abstractions.Core.Domain.Interfaces.Services;
using BAYSOFT.Abstractions.Core.Domain.Validations;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace BAYSOFT.Abstractions.Core.Domain.Services
{
    public abstract class DomainService<TEntity> : IDomainService<TEntity>
        where TEntity : DomainEntity
    {
        private IStringLocalizer Localizer { get; set; }
        private EntityValidator<TEntity> EntityValidator { get; set; }
        private DomainValidator<TEntity> DomainValidator { get; set; }
        public DomainService(IStringLocalizer localizer, EntityValidator<TEntity> entityValidator, DomainValidator<TEntity> domainValidator)
        {
            Localizer = localizer;
            EntityValidator = entityValidator;
            DomainValidator = domainValidator;
        }

        protected bool ValidateEntity(TEntity entity, bool throwException = true, string message = null)
        {
            var result = EntityValidator.Validate(entity);

            if (!result.IsValid && throwException)
            {
                throw new BusinessException(
                    message ?? Localizer["Operation failed in entity validation!"],
                    result.Errors.Select(error =>
                        new EntityValidationException(Localizer[error.PropertyName], string.Format(Localizer[error.ErrorMessage], Localizer[error.PropertyName]))
                    ).ToList());
            }

            return result.IsValid;
        }

        protected bool ValidateDomain(TEntity entity, bool throwException = true, string message = null)
        {
            var result = DomainValidator.Validate(entity);

            if (!result.IsValid && throwException)
            {
                throw new BusinessException(
                    message ?? Localizer["Operation failed in domain validation!"],
                    result.Errors.Select(error =>
                        new DomainValidationException(string.Format(Localizer[error.ErrorMessage], Localizer[error.PropertyName]))
                    ).ToList());
            }

            return result.IsValid;
        }
        public abstract Task Run(TEntity entity);
    }
}
