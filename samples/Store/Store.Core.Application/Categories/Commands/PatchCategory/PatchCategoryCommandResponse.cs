using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommandResponse : WrapResponse<Category>
    {
        public PatchCategoryCommandResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
