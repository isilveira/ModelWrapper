using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryResponse : ResponseBase<Customer>
    {
        public GetCustomerByIDQueryResponse(WrapRequest<Customer> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
