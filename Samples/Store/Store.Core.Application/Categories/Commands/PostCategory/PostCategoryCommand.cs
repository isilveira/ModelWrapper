using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommand : WrapRequest<Category>, IRequest<PostCategoryCommandResponse>
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
