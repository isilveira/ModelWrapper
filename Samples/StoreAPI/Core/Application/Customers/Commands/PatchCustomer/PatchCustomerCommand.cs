using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommand : Wrap<Customer>, IRequest<PatchCustomerCommandResponse>
    {
        public PatchCustomerCommand()
        {
            KeyProperty(x => x.CustomerID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.Orders);
        }
    }
}
