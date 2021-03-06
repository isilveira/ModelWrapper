using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : ApplicationRequest<Product, DeleteProductCommandResponse>
    {
        public DeleteProductCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.OrderedProducts);

            ConfigSuppressedResponseProperties(x => x.Images);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
