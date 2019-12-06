using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandResponse : ResponseBase<Customer>
    {
        public PutCustomerCommandResponse(WrapRequest<Customer> request, Customer data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
