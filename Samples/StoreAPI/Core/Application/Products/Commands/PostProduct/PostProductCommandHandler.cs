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
            var data = request.Post();

            await Context.Products.AddAsync(data);

            data.RegistrationDate = DateTime.UtcNow;

            await Context.SaveChangesAsync();

            return new PostProductCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.AllWithoutKeys),
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
