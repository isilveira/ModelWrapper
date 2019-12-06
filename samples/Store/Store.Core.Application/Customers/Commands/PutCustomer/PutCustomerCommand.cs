using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommand : RequestBase<Customer, PutCustomerCommandResponse>
    {
        public PutCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
