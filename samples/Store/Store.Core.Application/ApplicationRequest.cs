using MediatR;
using ModelWrapper;

namespace Store.Core.Application
{
    public abstract class ApplicationRequest<TEntity, TResponse> : WrapRequest<TEntity>, IRequest<TResponse>
        where TEntity : class
        where TResponse : ApplicationResponse<TEntity>
    {
    }
}
