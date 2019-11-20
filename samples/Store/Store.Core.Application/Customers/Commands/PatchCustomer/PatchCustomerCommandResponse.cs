﻿using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommandResponse : WrapResponse<Customer>
    {
        public PatchCustomerCommandResponse(WrapRequest<Customer> request, Customer data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
