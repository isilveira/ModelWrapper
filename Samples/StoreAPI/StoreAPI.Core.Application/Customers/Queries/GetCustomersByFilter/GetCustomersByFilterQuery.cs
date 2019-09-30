using EntitySearch;
using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : WrapRequest<Customer>, IRequest<GetCustomersByFilterQueryResponse>
    {
        public GetCustomersByFilterQuery()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
