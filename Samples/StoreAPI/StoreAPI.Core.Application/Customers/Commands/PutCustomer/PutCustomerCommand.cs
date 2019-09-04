using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommand : Wrap<Customer>, IRequest<PutCustomerCommandResponse>
    {
        public PutCustomerCommand()
        {
            KeyProperty(x => x.CustomerID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.Orders);
        }
    }
}
