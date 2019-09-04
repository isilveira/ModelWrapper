using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommand : Wrap<OrderedProduct>, IRequest<PostOrderedProductCommandResponse>
    {
        public PostOrderedProductCommand()
        {
            KeyProperty(x => x.OrderedProductID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.Order);
            SuppressProperty(x => x.Product);
        }
    }
}
