using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommandHandler : ApplicationRequestHandler<OrderedProduct, PostOrderedProductCommand, PostOrderedProductCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostOrderedProductService PostService { get; set; }
        public PostOrderedProductCommandHandler(
            IDefaultDbContext context,
            IPostOrderedProductService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostOrderedProductCommandResponse> Handle(PostOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
