using BAYSOFT.Abstractions.Core.Domain.Entities;
using NetDevPack.Specification;

namespace BAYSOFT.Abstractions.Core.Domain.Validations
{
    public abstract class DomainSpecification<TEntity> : Specification<TEntity>
        where TEntity : DomainEntity
    {
    }
}
