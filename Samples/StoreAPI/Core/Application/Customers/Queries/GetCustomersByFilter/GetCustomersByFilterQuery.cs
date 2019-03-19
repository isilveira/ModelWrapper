using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : IRequest<GetCustomersByFilterQueryResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}
