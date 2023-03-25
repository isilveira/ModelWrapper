using BAYSOFT.Abstractions.Core.Domain.Entities;
using NetDevPack.Specification;

namespace BAYSOFT.Abstractions.Core.Domain.Validations
{
    public class DomainRule<TEntity> : Rule<TEntity>
        where TEntity : DomainEntity
    {
        public DomainRule(Specification<TEntity> spec, string errorMessage) : base(spec, errorMessage)
        {
        }
    }
}
