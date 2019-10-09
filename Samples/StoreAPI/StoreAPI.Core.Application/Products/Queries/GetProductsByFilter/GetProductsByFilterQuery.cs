using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQuery: WrapRequest<Product>, IRequest<GetProductsByFilterQueryResponse>
    {
        public GetProductsByFilterQuery()
        {
            ConfigKeys(x => x.ProductID);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.Category);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
            ConfigSuppressedResponseProperties(x => x.Images);
            ConfigSuppressedResponseProperties(x => x.Category);
        }
    }
}
