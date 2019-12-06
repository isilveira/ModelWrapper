using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommand : RequestBase<Product, PatchProductCommandResponse>
    {
        public PatchProductCommand()
        {
            ConfigKeys(x => x.ProductID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Category);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.OrderedProducts);

            ConfigSuppressedResponseProperties(x => x.RegistrationDate);
            ConfigSuppressedResponseProperties(x => x.Category);
            ConfigSuppressedResponseProperties(x => x.Images);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
