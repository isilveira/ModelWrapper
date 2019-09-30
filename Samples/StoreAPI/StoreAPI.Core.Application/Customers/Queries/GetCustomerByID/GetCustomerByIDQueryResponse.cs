using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryResponse : WrapResponse<Customer>
    {
        public GetCustomerByIDQueryResponse(
            WrapRequest<Customer> request,
            Customer data) : base(request, data, null, 1)
        {
        }
    }
}
