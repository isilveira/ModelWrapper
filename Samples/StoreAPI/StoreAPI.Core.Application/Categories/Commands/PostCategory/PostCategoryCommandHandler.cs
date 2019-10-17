using MediatR;
using ModelWrapper.Extensions.Post;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommandHandler : IRequestHandler<PostCategoryCommand, PostCategoryCommandResponse>
    {
        public IStoreContext Context { get; set; }
        public PostCategoryCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostCategoryCommandResponse> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();
            
            await Context.Categories.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
