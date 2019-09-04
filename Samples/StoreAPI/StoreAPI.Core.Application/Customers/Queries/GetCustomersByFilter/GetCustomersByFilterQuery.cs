using EntitySearch;
using MediatR;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : EntitySearch<Customer>, IRequest<GetCustomersByFilterQueryResponse>
    {
        public GetCustomersByFilterQuery()
        {
            SetRestrictProperty(x => x.Orders);
        }
    }
}
