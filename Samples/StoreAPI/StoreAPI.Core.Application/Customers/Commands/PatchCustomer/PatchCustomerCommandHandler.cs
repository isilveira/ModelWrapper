using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var id = request.Project(x => x.CustomerID);
            var data = await Context.Customers.SingleOrDefaultAsync(x => x.CustomerID == id);

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchCustomerCommandResponse(request, data, "Successful operation!");
        }
    }
}
