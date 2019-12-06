using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : RequestBase<Product, DeleteProductCommandResponse>
    {
        public DeleteProductCommand()
        {
            ConfigKeys(x => x.ProductID);

            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.Category);

            ConfigSuppressedResponseProperties(x => x.Category);
            ConfigSuppressedResponseProperties(x => x.Images);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
