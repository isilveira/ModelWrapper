using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var id = request.Project(x => x.OrderID);

            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID == id);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
