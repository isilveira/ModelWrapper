using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PostCustomer
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
