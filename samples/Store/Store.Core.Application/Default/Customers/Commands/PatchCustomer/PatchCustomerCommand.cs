using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommand : ApplicationRequest<Customer, PatchCustomerCommandResponse>
    {
        public PatchCustomerCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
