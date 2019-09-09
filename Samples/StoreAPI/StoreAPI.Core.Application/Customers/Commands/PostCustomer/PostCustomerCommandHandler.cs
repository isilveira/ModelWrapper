using MediatR;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var response = new PostCustomerCommandResponse();

            return new PostCustomerCommandResponse(request, new Customer { CustomerID = 1, Email = "italobrian@gmail.com", Name = "Ítalo Silveira" }, "Successful operation!");
            //var data = request.Post();

            //data.RegistrationDate = DateTime.UtcNow;

            //await Context.Customers.AddAsync(data);

            //await Context.SaveChangesAsync();

            //return new PostCustomerCommandResponse
            //{
            //    Message = "Successful operation!",
            //    Request = request.AsDictionary(ModelWrapper.EnumProperties.AllWithoutKeys),
            //    Data = new PostCustomerCommandResponseDTO
            //    {
            //        CustomerID = data.CustomerID,
            //        Name = data.Name,
            //        Email = data.Email
            //    }
            //};
        }
    }
}
