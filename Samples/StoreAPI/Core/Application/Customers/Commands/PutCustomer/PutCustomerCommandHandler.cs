﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandHandler : IRequestHandler<PutCustomerCommand, PutCustomerCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutCustomerCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutCustomerCommandResponse> Handle(PutCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Customers.SingleOrDefaultAsync(x => x.CustomerID== request.GetID());

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutCustomerCommandResponse
            {
                Request = request.AsDictionary(),
                Message = "Successful operation!",
                Data = new PutCustomerCommandResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }
            };
        }
    }
}
