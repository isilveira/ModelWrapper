using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandResponse : WrapResponse<Customer>
    {
        public PutCustomerCommandResponse(WrapRequest<Customer> request, Customer data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
