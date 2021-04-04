using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.PutProduct
{
    public class PutProductCommand : ApplicationRequest<Product, PutProductCommandResponse>
    {
        public PutProductCommand()
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
