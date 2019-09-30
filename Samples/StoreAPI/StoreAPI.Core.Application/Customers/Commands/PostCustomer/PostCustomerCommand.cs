using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : WrapRequest<Customer>,IRequest<PostCustomerCommandResponse>
    {
        public PostCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
