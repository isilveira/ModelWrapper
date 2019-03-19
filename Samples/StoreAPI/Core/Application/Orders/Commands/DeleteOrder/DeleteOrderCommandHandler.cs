using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteOrderCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID== request.OrderID);

            if (data == null)
                throw new Exception("Customer not found!");

            Context.Orders.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new DeleteOrderCommandResponseDTO
                {
                    OrderID = data.OrderID,
                    CustomerID = data.CustomerID,
                    RegistrationDate = data.RegistrationDate,
                    ConfirmationDate = data.ConfirmationDate,
                    CancellationDate = data.CancellationDate
                }
            };
        }
    }
}
