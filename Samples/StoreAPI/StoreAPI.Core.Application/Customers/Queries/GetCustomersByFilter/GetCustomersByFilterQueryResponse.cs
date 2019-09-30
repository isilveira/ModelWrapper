using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryResponse : WrapResponse<Customer>
    {
        public GetCustomersByFilterQueryResponse(
            WrapRequest<Customer> request,
            List<Customer> customers,
            long resultCount
        ) : base(request, customers, null, resultCount)
        {

        }
    }
}
