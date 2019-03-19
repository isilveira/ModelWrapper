using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
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

            return new PostCategoryCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.AllWithoutKeys),
                Data = new PostCategoryCommandResponseDTO
                {
                    CategoryID = data.CategoryID,
                    RootCategoryID = data.RootCategoryID,
                    Name = data.Name,
                    Description = data.Description
                }
            };
        }
    }
}
