using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Customers;
using ModelWrapper.Extensions.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandHandler : ApplicationRequestHandler<Customer, PostCustomerCommand, PostCustomerCommandResponse>
    {
        private IDefaultDbContext Context { get; set; }
        private IPostCustomerService PostService { get; set; }
        public PostCustomerCommandHandler(
            IDefaultDbContext context,
            IPostCustomerService postService
        )
        {
            Context = context;
            PostService = postService;
        }
        public override async Task<PostCustomerCommandResponse> Handle(PostCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            await PostService.Run(data);

            await Context.SaveChangesAsync();

            return new PostCustomerCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
