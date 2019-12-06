using MediatR;
using ModelWrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Application.Bases
{
    public abstract class RequestBase<TEntity,TResponse> : WrapRequest<TEntity>, IRequest<TResponse>
        where TEntity : class
        where TResponse : ResponseBase<TEntity>
    {
    }
}
