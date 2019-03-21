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
            var id = request.Project(x => x.OrderID);
            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID == id);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchOrderCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.OnlySupplieds),
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
