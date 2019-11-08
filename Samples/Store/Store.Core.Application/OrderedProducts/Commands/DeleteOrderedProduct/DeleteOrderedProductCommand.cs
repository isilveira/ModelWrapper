using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
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
