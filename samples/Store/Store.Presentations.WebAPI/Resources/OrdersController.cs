using Store.Core.Application.Default.Orders.Commands.DeleteOrder;
using Store.Core.Application.Default.Orders.Commands.PatchOrder;
using Store.Core.Application.Default.Orders.Commands.PostOrder;
using Store.Core.Application.Default.Orders.Commands.PutOrder;
using Store.Core.Application.Default.Orders.Queries.GetOrderByID;
using Store.Core.Application.Default.Orders.Queries.GetOrdersByFilter;
using Store.Presentations.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Resources
{
    public class OrdersController : ResourceController
    {
        [HttpGet]
        public async Task<ActionResult<GetOrdersByFilterQueryResponse>> Get(GetOrdersByFilterQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderByIDQueryResponse>> Get(GetOrderByIDQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<PostOrderCommandResponse>> Post(PostOrderCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutOrderCommandResponse>> Put(PutOrderCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchOrderCommandResponse>> Patch(PatchOrderCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteOrderCommandResponse>> Delete(DeleteOrderCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
