using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommand : Wrap<OrderedProduct, int>, IRequest<PostOrderedProductCommandResponse>
    {
        public PostOrderedProductCommand()
        {
            SuppressProperty(x => x.OrderedProductID);
        }
    }
}
