using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PutCustomer
{
    public class PutCustomerCommand : ApplicationRequest<Customer, PutCustomerCommandResponse>
    {
        public PutCustomerCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
