using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Commands.PostCategory
{
    public class PostCategoryCommandResponse : ApplicationResponse<Category>
    {
        public PostCategoryCommandResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
