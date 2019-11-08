using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.PutCategory
{
    public class PutCategoryCommandResponse : WrapResponse<Category>
    {
        public PutCategoryCommandResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
