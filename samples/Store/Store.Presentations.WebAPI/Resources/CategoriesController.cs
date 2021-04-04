using Store.Core.Application.Default.Categories.Commands.DeleteCategory;
using Store.Core.Application.Default.Categories.Commands.PatchCategory;
using Store.Core.Application.Default.Categories.Commands.PostCategory;
using Store.Core.Application.Default.Categories.Commands.PutCategory;
using Store.Core.Application.Default.Categories.Queries.GetCategoryByID;
using Store.Core.Application.Default.Categories.Queries.GetCategoriesByFilter;
using Store.Presentations.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Resources
{
    public class CategoriesController : ResourceController
    {
        [HttpGet]
        public async Task<ActionResult<GetCategoriesByFilterQueryResponse>> Get(GetCategoriesByFilterQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryByIDQueryResponse>> Get(GetCategoryByIDQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<PostCategoryCommandResponse>> Post(PostCategoryCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutCategoryCommandResponse>> Put(PutCategoryCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchCategoryCommandResponse>> Patch(PatchCategoryCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCategoryCommandResponse>> Delete(DeleteCategoryCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
