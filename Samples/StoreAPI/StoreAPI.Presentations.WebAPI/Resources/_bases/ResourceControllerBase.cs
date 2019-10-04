using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Resources._Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceControllerBase: ControllerBase
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
                return BadRequest(ex);
            }
        }
    }
}
