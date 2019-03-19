using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommand : Wrap<Product, int>, IRequest<PostProductCommandResponse>
    {
        public PostProductCommand()
        {
            SuppressProperty(x => x.ProductID);
        }
    }
}
