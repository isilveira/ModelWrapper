using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : RequestBase<Customer,PostCustomerCommandResponse>
    {
        public PostCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);

            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
