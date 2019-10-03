using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommand : WrapRequest<OrderedProduct>, IRequest<DeleteOrderedProductCommandResponse>
    {
        protected DeleteOrderedProductCommand()
        {
            ConfigKeys(x => x.OrderedProductID);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
