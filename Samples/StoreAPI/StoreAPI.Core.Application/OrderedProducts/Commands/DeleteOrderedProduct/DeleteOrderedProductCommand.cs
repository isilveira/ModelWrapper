using MediatR;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommand : IRequest<DeleteOrderedProductCommandResponse>
    {
        public int OrderedProductID { get; set; }
    }
}
