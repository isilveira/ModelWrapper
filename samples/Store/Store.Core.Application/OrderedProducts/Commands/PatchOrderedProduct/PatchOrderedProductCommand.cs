using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommand : RequestBase<OrderedProduct, PatchOrderedProductCommandResponse>
    {
        public PatchOrderedProductCommand()
        {
            ConfigKeys(x => x.OrderedProductID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
