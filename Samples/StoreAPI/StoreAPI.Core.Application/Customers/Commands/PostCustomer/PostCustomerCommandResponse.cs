using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandResponse : WrapResponse<Customer>
    {
        public PostCustomerCommandResponse(WrapRequest<Customer> request, Customer data, string message) : base(request, data, message)
        {

        }
    }
}
