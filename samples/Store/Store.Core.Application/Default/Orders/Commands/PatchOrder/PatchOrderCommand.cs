using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : ApplicationRequest<Order, PatchOrderCommandResponse>
    {
        public PatchOrderCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Customer);
            ConfigSuppressedProperties(x => x.OrderedProducts);

            ConfigSuppressedResponseProperties(x => x.Customer);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
