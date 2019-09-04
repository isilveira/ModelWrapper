using EntitySearch;
using MediatR;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQuery : EntitySearch<Order>, IRequest<GetOrdersByFilterQueryResponse>
    {
        public GetOrdersByFilterQuery()
        {
            SetRestrictProperty(x => x.Customer);
            SetRestrictProperty(x => x.OrderedProducts);
        }
    }
}
