using MediatR;
using ModelWrapper.Extensions.Post;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, PostOrderCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostOrderCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostOrderCommandResponse> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            data.RegistrationDate = DateTime.UtcNow;

            await Context.Orders.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
