using EntitySearch;
using MediatR;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQuery: EntitySearch<Product>,IRequest<GetProductsByFilterQueryResponse>
    {
        public GetProductsByFilterQuery()
        {
            SetRestrictProperty(x => x.Category);
            SetRestrictProperty(x => x.Images);
            SetRestrictProperty(x => x.OrderedProducts);
        }
    }
}
