using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : Wrap<Customer,int>,IRequest<PostCustomerCommandResponse>
    {
        public PostCustomerCommand()
        {
            SuppressProperty(x => x.CustomerID);
        }
    }
}
