using MediatR;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommand : IRequest<PostOrderedProductCommandResponse>
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int Amount { get; set; }
        public decimal Value { get; set; }
    }
}
