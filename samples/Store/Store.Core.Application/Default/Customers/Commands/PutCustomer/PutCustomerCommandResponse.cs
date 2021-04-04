using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandResponse : ApplicationResponse<Customer>
    {
        public PutCustomerCommandResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
