using MediatR;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
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
            var data = request.Post();

            data.RegistrationDate = DateTime.UtcNow;

            await Context.OrderedProducts.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
