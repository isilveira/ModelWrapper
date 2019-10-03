using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var id = request.Project(x => x.OrderID);

            var data = await Context.Orders.SingleOrDefaultAsync(x => x.OrderID == id);

            if (data == null)
                throw new Exception("Customer not found!");

            Context.Orders.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
