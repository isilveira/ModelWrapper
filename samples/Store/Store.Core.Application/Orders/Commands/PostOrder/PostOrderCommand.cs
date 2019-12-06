using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommand : RequestBase<Order, PostOrderCommandResponse>
    {
        public PostOrderCommand()
        {
            ConfigKeys(x => x.OrderID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.CancellationDate);
            ConfigSuppressedProperties(x => x.ConfirmationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
