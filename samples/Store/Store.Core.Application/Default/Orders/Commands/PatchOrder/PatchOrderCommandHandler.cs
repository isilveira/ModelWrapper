using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Orders;

namespace Store.Core.Application.Default.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandHandler : ApplicationRequestHandler<Order, PatchOrderCommand, PatchOrderCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchOrderService PatchService { get; set; }
        public PatchOrderCommandHandler(
            IDefaultDbContext context,
            IPatchOrderService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchOrderCommandResponse> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchOrderCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
