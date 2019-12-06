using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommand : RequestBase<Order, PutOrderCommandResponse>
    {
        public PutOrderCommand()
        {
            ConfigKeys(x => x.OrderID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
