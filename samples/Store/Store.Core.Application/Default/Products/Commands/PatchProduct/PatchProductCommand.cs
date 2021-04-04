using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.PatchProduct
{
    public class PatchProductCommand : ApplicationRequest<Product, PatchProductCommandResponse>
    {
        public PatchProductCommand()
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
