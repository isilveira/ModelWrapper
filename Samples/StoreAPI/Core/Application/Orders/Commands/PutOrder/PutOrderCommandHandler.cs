using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommandHandler : IRequestHandler<PutOrderCommand, PutOrderCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutOrderCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutOrderCommandResponse> Handle(PutOrderCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID == request.OrderID);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            data.CustomerID = request.CustomerID;
            data.ConfirmationDate = request.ConfirmationDate;
            data.CancellationDate = request.CancellationDate;

            await Context.SaveChangesAsync();

            return new PutOrderCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PutOrderCommandResponseDTO
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
