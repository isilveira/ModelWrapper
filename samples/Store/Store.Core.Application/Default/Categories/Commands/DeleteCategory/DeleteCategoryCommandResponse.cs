using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandResponse : ApplicationResponse<Category>
    {
        public DeleteCategoryCommandResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
