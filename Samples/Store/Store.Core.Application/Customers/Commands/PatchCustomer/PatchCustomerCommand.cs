using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommand : WrapRequest<Customer>, IRequest<PatchCustomerCommandResponse>
    {
        public PatchCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
