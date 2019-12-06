using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandResponse : ResponseBase<Customer>
    {
        public PostCustomerCommandResponse(WrapRequest<Customer> request, Customer data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
