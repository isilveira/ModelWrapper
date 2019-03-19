using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PutProduct
{
    public class PutProductCommand : Wrap<Product, int>, IRequest<PutProductCommandResponse>
    {
        public PutProductCommand()
        {
            SuppressProperty(x => x.ProductID);
        }
    }
}
