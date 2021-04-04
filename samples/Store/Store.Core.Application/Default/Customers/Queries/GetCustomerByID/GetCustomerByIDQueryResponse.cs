using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryResponse : ApplicationResponse<Customer>
    {
        public GetCustomerByIDQueryResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
