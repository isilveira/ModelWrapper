using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Products.Queries.GetProductByID
{
    public class GetProductByIDQuery : WrapRequest<Product>, IRequest<GetProductByIDQueryResponse>
    {
        public GetProductByIDQuery()
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
