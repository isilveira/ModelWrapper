using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommandHandler : IRequestHandler<PostOrderedProductCommand, PostOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostOrderedProductCommandResponse> Handle(PostOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var data = new OrderedProduct
            {
                OrderID = request.OrderID,
                ProductID = request.ProductID,
                Amount = request.Amount,
                Value = request.Value,
                RegistrationDate = DateTime.UtcNow
            };

            await Context.OrderedProducts.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostOrderedProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PostOrderedProductCommandResponseDTO
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
