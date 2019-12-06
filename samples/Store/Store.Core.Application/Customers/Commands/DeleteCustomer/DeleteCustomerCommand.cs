using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : RequestBase<Customer, DeleteCustomerCommandResponse>
    {
        public DeleteCustomerCommand()
        {
            ConfigKeys(x => x.CustomerID);

            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
