using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : WrapRequest<Category>, IRequest<DeleteCategoryCommandResponse>
    {
        public DeleteCategoryCommand()
        {
            ConfigKeys(x => x.CategoryID);
            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
        }
    }
}
