using BAYSOFT.Abstractions.Core.Domain.Entities;
using FluentValidation;

namespace BAYSOFT.Abstractions.Core.Domain.Validations
{
    public abstract class EntityValidator<TEntity> : AbstractValidator<TEntity>
        where TEntity : DomainEntity
    {
    }
}
