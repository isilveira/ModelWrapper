using EntitySearch;
using MediatR;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQuery : EntitySearch<OrderedProduct>, IRequest<GetOrderedProductsByFilterQueryResponse>
    {
        public GetOrderedProductsByFilterQuery()
        {
            SetRestrictProperty(x => x.Product);
            SetRestrictProperty(x => x.Order);
        }
    }
}
