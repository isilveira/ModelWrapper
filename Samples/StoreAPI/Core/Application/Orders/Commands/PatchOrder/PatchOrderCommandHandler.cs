using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandHandler : IRequestHandler<PatchOrderCommand, PatchOrderCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchOrderCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchOrderCommandResponse> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID == request.OrderID);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            if (request.CustomerID.HasValue)
            {
                data.CustomerID = request.CustomerID.Value;
            }
            if (request.ConfirmationDate.HasValue)
            {
                data.ConfirmationDate = request.ConfirmationDate.Value;
            }
            if (request.CancellationDate.HasValue)
            {
                data.CancellationDate = request.CancellationDate.Value;
            }

            await Context.SaveChangesAsync();

            return new PatchOrderCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PatchOrderCommandResponseDTO
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
