using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
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

            return new PostCustomerCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.AllWithoutKeys),
                Data = new PostCustomerCommandResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }
            };
        }
    }
}
