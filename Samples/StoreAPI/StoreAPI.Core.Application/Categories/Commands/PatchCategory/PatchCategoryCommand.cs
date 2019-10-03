using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommand : WrapRequest<Category>, IRequest<PatchCategoryCommandResponse>
    {
        public PatchCategoryCommand()
        {
            ConfigKeys(x => x.CategoryID);
            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
        }
    }
}
