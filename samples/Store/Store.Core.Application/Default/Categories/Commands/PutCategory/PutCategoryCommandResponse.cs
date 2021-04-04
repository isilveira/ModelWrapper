using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Commands.PutCategory
{
    public class PutCategoryCommandResponse : ApplicationResponse<Category>
    {
        public PutCategoryCommandResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
