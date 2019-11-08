using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommand : WrapRequest<Customer>, IRequest<PutCustomerCommandResponse>
    {
        public PutCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
