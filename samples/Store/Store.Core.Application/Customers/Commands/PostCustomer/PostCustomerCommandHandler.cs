using MediatR;
using ModelWrapper.Extensions.Post;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandHandler : IRequestHandler<PostCustomerCommand, PostCustomerCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostCustomerCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostCustomerCommandResponse> Handle(PostCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = request.Post();

            data.RegistrationDate = DateTime.UtcNow;

            await Context.Customers.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostCustomerCommandResponse(request, data, "Successful operation!");
        }
    }
}
