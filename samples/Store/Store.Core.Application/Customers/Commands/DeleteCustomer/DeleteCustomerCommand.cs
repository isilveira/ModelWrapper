using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : WrapRequest<Customer>, IRequest<DeleteCustomerCommandResponse>
    {
        public DeleteCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
