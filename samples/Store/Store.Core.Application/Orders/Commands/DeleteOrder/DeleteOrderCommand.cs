using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : RequestBase<Order, DeleteOrderCommandResponse>
    {
        protected DeleteOrderCommand()
        {
            ConfigKeys(x => x.OrderID);

            ConfigSuppressedProperties(x => x.Customer);
            ConfigSuppressedProperties(x => x.OrderedProducts);
        }
    }
}
