using Store.Core.Application.Default.Images.Commands.DeleteImage;
using Store.Core.Application.Default.Images.Commands.PatchImage;
using Store.Core.Application.Default.Images.Commands.PostImage;
using Store.Core.Application.Default.Images.Commands.PutImage;
using Store.Core.Application.Default.Images.Queries.GetImageByID;
using Store.Core.Application.Default.Images.Queries.GetImagesByFilter;
using Store.Presentations.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Resources
{
    public class ImagesController : ResourceController
    {
        [HttpGet]
        public async Task<ActionResult<GetImagesByFilterQueryResponse>> Get(GetImagesByFilterQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetImageByIDQueryResponse>> Get(GetImageByIDQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<PostImageCommandResponse>> Post(PostImageCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutImageCommandResponse>> Put(PutImageCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchImageCommandResponse>> Patch(PatchImageCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteImageCommandResponse>> Delete(DeleteImageCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
