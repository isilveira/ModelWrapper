using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Categories;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Categories.Commands.PostCategory
{
    public class PostCategoryCommandHandler : ApplicationRequestHandler<Category, PostCategoryCommand, PostCategoryCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostCategoryService PostService { get; set; }
        public PostCategoryCommandHandler(
            IDefaultDbContext context,
            IPostCategoryService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostCategoryCommandResponse> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
