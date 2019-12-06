using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommand : RequestBase<Category, PostCategoryCommandResponse>
    {
        public PostCategoryCommand()
        {
            ConfigKeys(x => x.CategoryID);

            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
        }
    }
}
