using Store.Core.Application.Default.OrderedProducts.Commands.DeleteOrderedProduct;
using Store.Core.Application.Default.OrderedProducts.Commands.PatchOrderedProduct;
using Store.Core.Application.Default.OrderedProducts.Commands.PostOrderedProduct;
using Store.Core.Application.Default.OrderedProducts.Commands.PutOrderedProduct;
using Store.Core.Application.Default.OrderedProducts.Queries.GetOrderedProductByID;
using Store.Core.Application.Default.OrderedProducts.Queries.GetOrderedProductsByFilter;
using Store.Presentations.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Resources
{
    public class OrderedProductsController : ResourceController
    {
        [HttpGet]
        public async Task<ActionResult<GetOrderedProductsByFilterQueryResponse>> Get(GetOrderedProductsByFilterQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderedProductByIDQueryResponse>> Get(GetOrderedProductByIDQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<PostOrderedProductCommandResponse>> Post(PostOrderedProductCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutOrderedProductCommandResponse>> Put(PutOrderedProductCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchOrderedProductCommandResponse>> Patch(PatchOrderedProductCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteOrderedProductCommandResponse>> Delete(DeleteOrderedProductCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
