using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommandHandler : IRequestHandler<PatchCustomerCommand, PatchCustomerCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchCustomerCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchCustomerCommandResponse> Handle(PatchCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Customers.SingleOrDefaultAsync(x => x.CustomerID== request.CustomerID);

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }
            
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                data.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                data.Email = request.Email;
            }

            await Context.SaveChangesAsync();

            return new PatchCustomerCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PatchCustomerCommandResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }
            };
        }
    }
}
