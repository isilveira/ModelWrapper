using MediatR;
using ModelWrapper.Extensions.Post;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommandHandler : IRequestHandler<PostImageCommand, PostImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostImageCommandResponse> Handle(PostImageCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await Context.Images.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
