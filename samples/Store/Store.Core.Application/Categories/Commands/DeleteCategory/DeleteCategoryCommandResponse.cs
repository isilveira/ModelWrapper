using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandResponse : ResponseBase<Category>
    {
        public DeleteCategoryCommandResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
