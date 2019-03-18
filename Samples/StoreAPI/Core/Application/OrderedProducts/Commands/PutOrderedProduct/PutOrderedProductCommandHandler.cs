using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommandHandler : IRequestHandler<PutOrderedProductCommand, PutOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutOrderedProductCommandResponse> Handle(PutOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == request.OrderedProductID);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            data.OrderID = request.OrderID;
            data.ProductID = request.ProductID;
            data.Amount = request.Amount;
            data.Value = request.Value;

            await Context.SaveChangesAsync();

            return new PutOrderedProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PutOrderedProductCommandResponseDTO
                {
                    OrderedProductID = data.OrderedProductID,
                    OrderID = data.OrderID,
                    ProductID = data.ProductID,
                    Amount = data.Amount,
                    Value = data.Value,
                    RegistrationDate = data.RegistrationDate
                }
            };
        }
    }
}
