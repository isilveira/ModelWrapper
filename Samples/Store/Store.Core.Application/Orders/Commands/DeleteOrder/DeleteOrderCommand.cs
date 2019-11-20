using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : WrapRequest<Order>, IRequest<DeleteOrderCommandResponse>
    {
        protected DeleteOrderCommand()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.Customer);
            ConfigSuppressedProperties(x => x.OrderedProducts);
        }
    }
}
