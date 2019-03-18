using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteCustomerCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Customers.SingleOrDefaultAsync(x => x.CustomerID== request.CustomerID);

            if (data == null)
                throw new Exception("Customer not found!");

            Context.Customers.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteCustomerCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new DeleteCustomerCommandResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }
            };
        }
    }
}
