using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PostCustomer
{
    public class PostCustomerCommand : ApplicationRequest<Customer, PostCustomerCommandResponse>
    {
        public PostCustomerCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
