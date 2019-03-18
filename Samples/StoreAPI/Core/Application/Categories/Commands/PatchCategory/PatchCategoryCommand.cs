using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommand : Wrap<Category, int>, IRequest<PatchCategoryCommandResponse>
    {
        //public int CategoryID { get; set; }
    }
}
