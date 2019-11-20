using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Presentations.StoreAPP.Controllers.Bases
{
    public class MediatorControllerBase : Controller
    {
        private IMediator _mediator;
        private IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public async Task<ActionResult<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return Ok(await Mediator.Send(request, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
