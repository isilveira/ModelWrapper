using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Products;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Products.Commands.PostProduct
{
    public class PostProductCommandHandler : ApplicationRequestHandler<Product, PostProductCommand, PostProductCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostProductService PostService { get; set; }
        public PostProductCommandHandler(
            IDefaultDbContext context,
            IPostProductService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostProductCommandResponse> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
