using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Orders;

namespace Store.Core.Application.Default.Orders.Commands.PutOrder
{
    public class PutOrderCommandHandler : ApplicationRequestHandler<Order, PutOrderCommand, PutOrderCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutOrderService PutService { get; set; }
        public PutOrderCommandHandler(
            IDefaultDbContext context,
            IPutOrderService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutOrderCommandResponse> Handle(PutOrderCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
