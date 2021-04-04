using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommand : ApplicationRequest<OrderedProduct, DeleteOrderedProductCommandResponse>
    {
        public DeleteOrderedProductCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);
            ConfigSuppressedProperties(x => x.Order);

            ConfigSuppressedResponseProperties(x => x.Product);
            ConfigSuppressedResponseProperties(x => x.Order);
        }
    }
}
