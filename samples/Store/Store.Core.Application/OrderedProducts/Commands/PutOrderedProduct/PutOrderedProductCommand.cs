using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommand : RequestBase<OrderedProduct, PutOrderedProductCommandResponse>
    {
        public PutOrderedProductCommand()
        {
            ConfigKeys(x => x.OrderedProductID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
