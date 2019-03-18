using MediatR;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : IRequest<PostCustomerCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
