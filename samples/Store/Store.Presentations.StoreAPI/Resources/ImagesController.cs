using Microsoft.AspNetCore.Mvc;
using Store.Core.Application.Images.Commands.DeleteImage;
using Store.Core.Application.Images.Commands.PatchImage;
using Store.Core.Application.Images.Commands.PostImage;
using Store.Core.Application.Images.Commands.PutImage;
using Store.Core.Application.Images.Queries.GetImageByID;
using Store.Core.Application.Images.Queries.GetImagesByFilter;
using Store.Presentations.StoreAPI.Resources.Bases;
using System.Threading.Tasks;

namespace Store.Presentations.StoreAPI.Resources
{
    public class Images : MediatorControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetImagesByFilterQueryResponse>> Get([FromQuery]GetImagesByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{imageid}")]
        public async Task<ActionResult<GetImageByIDQueryResponse>> Get([FromRoute] GetImageByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostImageCommandResponse>> Post([FromBody] PostImageCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{imageid}")]
        public async Task<ActionResult<PutImageCommandResponse>> Put([FromRoute]int imageID, [FromBody]PutImageCommand request)
        {
            request.Project(x => x.ImageID = imageID);
            return await Send(request);
        }

        [HttpPatch("{imageid}")]
        public async Task<ActionResult<PatchImageCommandResponse>> Patch([FromRoute]int imageID, [FromBody] PatchImageCommand request)
        {
            request.Project(x=>x.ImageID = imageID);
            return await Send(request);
        }

        [HttpDelete("{imageid}")]
        public async Task<ActionResult<DeleteImageCommandResponse>> Delete([FromRoute]DeleteImageCommand request)
        {
            return await Send(request);
        }
    }
}