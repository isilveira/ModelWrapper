using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQuery : RequestBase<OrderedProduct, GetOrderedProductByIDQueryResponse>
    {
        protected GetOrderedProductByIDQuery()
        {
            ConfigKeys(x => x.OrderedProductID);
            
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Order);
            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
