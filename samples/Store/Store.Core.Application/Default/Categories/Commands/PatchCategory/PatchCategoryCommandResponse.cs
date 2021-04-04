using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommandResponse : ApplicationResponse<Category>
    {
        public PatchCategoryCommandResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
