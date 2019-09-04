using Microsoft.AspNetCore.Mvc;
using StoreAPI.Core.Application.Categories.Commands.DeleteCategory;
using StoreAPI.Core.Application.Categories.Commands.PatchCategory;
using StoreAPI.Core.Application.Categories.Commands.PostCategory;
using StoreAPI.Core.Application.Categories.Commands.PutCategory;
using StoreAPI.Core.Application.Categories.Queries.GetCategoriesByFilter;
using StoreAPI.Core.Application.Categories.Queries.GetCategoryByID;
using StoreAPI.Resources._Bases;
using System.Threading.Tasks;

namespace StoreAPI.Resources
{
    public class CategoriesController : ResourceControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCategoriesByFilterQueryResponse>> Get([FromQuery]GetCategoriesByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{categoryid}")]
        public async Task<ActionResult<GetCategoryByIDQueryResponse>> Get([FromRoute] GetCategoryByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostCategoryCommandResponse>> Post([FromBody] PostCategoryCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{categoryid}")]
        public async Task<ActionResult<PutCategoryCommandResponse>> Put([FromRoute]int categoryID, [FromBody]PutCategoryCommand request)
        {
            request.Project(x => x.CategoryID = categoryID);
            return await Send(request);
        }

        [HttpPatch("{categoryid}")]
        public async Task<ActionResult<PatchCategoryCommandResponse>> Patch([FromRoute]int categoryID, [FromBody] PatchCategoryCommand request)
        {
            request.Project(x => x.CategoryID = categoryID);
            return await Send(request);
        }

        [HttpDelete("{categoryid}")]
        public async Task<ActionResult<DeleteCategoryCommandResponse>> Delete([FromRoute]DeleteCategoryCommand request)
        {
            return await Send(request);
        }
    }
}