using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandResponse : ApplicationResponse<Customer>
    {
        public PostCustomerCommandResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
