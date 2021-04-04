using Store.Core.Application.Default.Samples.Commands.DeleteSample;
using Store.Core.Application.Default.Samples.Commands.PatchSample;
using Store.Core.Application.Default.Samples.Commands.PostSample;
using Store.Core.Application.Default.Samples.Commands.PutSample;
using Store.Core.Application.Default.Samples.Queries.GetSampleByID;
using Store.Core.Application.Default.Samples.Queries.GetSamplesByFilter;
using Store.Presentations.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Resources
{
    public class SamplesController : ResourceController
    {
        [HttpGet]
        public async Task<ActionResult<GetSamplesByFilterQueryResponse>> Get(GetSamplesByFilterQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSampleByIDQueryResponse>> Get(GetSampleByIDQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<PostSampleCommandResponse>> Post(PostSampleCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutSampleCommandResponse>> Put(PutSampleCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchSampleCommandResponse>> Patch(PatchSampleCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteSampleCommandResponse>> Delete(DeleteSampleCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
