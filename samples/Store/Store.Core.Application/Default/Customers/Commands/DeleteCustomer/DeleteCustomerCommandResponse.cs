using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandResponse : ApplicationResponse<Customer>
    {
        public DeleteCustomerCommandResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
