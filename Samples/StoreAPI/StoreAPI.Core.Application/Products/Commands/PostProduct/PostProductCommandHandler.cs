using MediatR;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
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

            return new PostProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
