using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : ApplicationRequest<Customer, DeleteCustomerCommandResponse>
    {
        public DeleteCustomerCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);
            
            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
