using BAYSOFT.Abstractions.Core.Domain.Entities;
using NetDevPack.Specification;

namespace BAYSOFT.Abstractions.Core.Domain.Validations
{
    public abstract class DomainValidator<TEntity> : SpecValidator<TEntity>
        where TEntity : DomainEntity
    {
    }
}
