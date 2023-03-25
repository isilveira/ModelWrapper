using BAYSOFT.Abstractions.Core.Domain.Entities;
using System.Threading.Tasks;

namespace BAYSOFT.Abstractions.Core.Domain.Interfaces.Services
{
    public interface IDomainService<TEntity>
        where TEntity : DomainEntity
    {
        Task Run(TEntity entity);
    }
}
