using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryResponse : WrapResponse<Customer>
    {
        public GetCustomersByFilterQueryResponse(WrapRequest<Customer> request, IList<Customer> data, string message = null, long? resultCount = null) : base(request, data, message, resultCount)
        {
        }
    }
}
