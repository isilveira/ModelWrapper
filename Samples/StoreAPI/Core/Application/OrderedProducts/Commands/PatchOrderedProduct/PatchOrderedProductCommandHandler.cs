using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandHandler : IRequestHandler<PatchOrderedProductCommand, PatchOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchOrderedProductCommandResponse> Handle(PatchOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == request.OrderedProductID);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            if (request.OrderID.HasValue)
            {
                data.OrderID = request.OrderID.Value;
            }

            if (request.ProductID.HasValue)
            {
                data.ProductID = request.ProductID.Value;
            }

            if (request.Amount.HasValue)
            {
                data.Amount = request.Amount.Value;
            }

            if (request.Value.HasValue)
            {
                data.Value = request.Value.Value;
            }

            await Context.SaveChangesAsync();

            return new PatchOrderedProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PatchOrderedProductCommandResponseDTO
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
