using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryResponse : ApplicationResponse<Customer>
    {
        public GetCustomersByFilterQueryResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
