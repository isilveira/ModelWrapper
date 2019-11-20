using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryResponse : WrapResponse<Customer>
    {
        public GetCustomersByFilterQueryResponse(WrapRequest<Customer> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
