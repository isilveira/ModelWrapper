using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : WrapRequest<Customer>,IRequest<PostCustomerCommandResponse>
    {
        public PostCustomerCommand()
        {
            //KeyProperty(x => x.CustomerID);
            //SuppressProperty(x => x.RegistrationDate);
            //SuppressProperty(x => x.Orders);
        }
    }
}
