using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommand : Wrap<Category, int>, IRequest<PatchCategoryCommandResponse>
    {
        //public int CategoryID { get; set; }
        public PatchCategoryCommand()
        {
            SuppressProperty(x => x.CategoryID);
            SuppressProperty(x => x.LeafCategories);
            SuppressProperty(x => x.Products);
            SuppressProperty(x => x.RootCategory);
        }
    }
}
