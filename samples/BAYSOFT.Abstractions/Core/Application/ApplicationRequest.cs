using BAYSOFT.Abstractions.Core.Domain.Entities;
using BAYSOFT.Abstractions.Core.Domain.Exceptions;
using BAYSOFT.Abstractions.Core.Domain.Validations;
using MediatR;
using Microsoft.Extensions.Localization;
using ModelWrapper;
using System.Linq;

namespace BAYSOFT.Abstractions.Core.Application
{
    public abstract class ApplicationRequest<TEntity, TResponse> : WrapRequest<TEntity>, IRequest<TResponse>
        where TEntity : DomainEntity
        where TResponse : ApplicationResponse<TEntity>
    {
        protected ApplicationRequestValidator<TEntity> Validator { get; set; }
        public ApplicationRequest()
        {
            Validator = new ApplicationRequestValidator<TEntity>();
        }
        public bool IsValid(IStringLocalizer localizer, bool throwException = true, string message = null)
        {
            var result = this.Validator.Validate(this.GetModel());
            
            if (!result.IsValid && throwException)
            {
                throw new BusinessException(
                    message ?? localizer["Operation failed in request validation!"],
                    result.Errors.Select(error =>
                        new RequestValidationException(localizer[error.PropertyName], string.Format(localizer[error.ErrorMessage], localizer[error.PropertyName]))
                    ).ToList());
            }

            return result.IsValid;
        }
    }
}
