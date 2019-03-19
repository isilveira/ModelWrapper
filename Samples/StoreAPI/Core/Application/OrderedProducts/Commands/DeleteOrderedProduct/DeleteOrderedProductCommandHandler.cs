using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandHandler : IRequestHandler<DeleteOrderedProductCommand, DeleteOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteOrderedProductCommandResponse> Handle(DeleteOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == request.OrderedProductID);

            if (data == null)
                throw new Exception("OrderedProduct not found!");

            Context.OrderedProducts.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderedProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new DeleteOrderedProductCommandResponseDTO
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
