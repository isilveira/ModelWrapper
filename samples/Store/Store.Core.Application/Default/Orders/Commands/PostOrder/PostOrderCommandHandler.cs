using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Orders;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Orders.Commands.PostOrder
{
    public class PostOrderCommandHandler : ApplicationRequestHandler<Order, PostOrderCommand, PostOrderCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostOrderService PostService { get; set; }
        public PostOrderCommandHandler(
            IDefaultDbContext context,
            IPostOrderService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostOrderCommandResponse> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
