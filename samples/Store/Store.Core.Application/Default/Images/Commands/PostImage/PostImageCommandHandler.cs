using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Images;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Images.Commands.PostImage
{
    public class PostImageCommandHandler : ApplicationRequestHandler<Image, PostImageCommand, PostImageCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostImageService PostService { get; set; }
        public PostImageCommandHandler(
            IDefaultDbContext context,
            IPostImageService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostImageCommandResponse> Handle(PostImageCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
