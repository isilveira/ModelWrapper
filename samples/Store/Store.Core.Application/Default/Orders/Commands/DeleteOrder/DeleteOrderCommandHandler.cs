using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Orders;

namespace Store.Core.Application.Default.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : ApplicationRequestHandler<Order, DeleteOrderCommand, DeleteOrderCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteOrderService DeleteService { get; set; }
        public DeleteOrderCommandHandler(
            IDefaultDbContext context,
            IDeleteOrderService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Order not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
