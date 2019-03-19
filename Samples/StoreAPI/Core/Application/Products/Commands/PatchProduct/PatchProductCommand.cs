using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommand : Wrap<Product, int>, IRequest<PatchProductCommandResponse>
    {
        public PatchProductCommand()
        {
            SuppressProperty(x => x.ProductID);
        }
    }
}
