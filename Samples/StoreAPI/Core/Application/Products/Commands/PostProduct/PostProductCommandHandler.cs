using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommandHandler : IRequestHandler<PostProductCommand, PostProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostProductCommandResponse> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            var data = new Product
            {
                CategoryID = request.CategoryID,
                Name = request.Name,
                Description = request.Description,
                Specifications = request.Specifications,
                RegistrationDate = DateTime.UtcNow,
                Value = request.Value,
                Amount = request.Amount,
                IsVisible = request.IsVisible
            };

            await Context.Products.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PostProductCommandResponseDTO
                {
                    ProductID = data.ProductID,
                    CategoryID = data.CategoryID,
                    Name = data.Name,
                    Description = data.Description,
                    Specifications = data.Specifications,
                    RegistrationDate = data.RegistrationDate,
                    Value = data.Value,
                    Amount = data.Amount,
                    IsVisible = data.IsVisible
                }
            };
        }
    }
}
