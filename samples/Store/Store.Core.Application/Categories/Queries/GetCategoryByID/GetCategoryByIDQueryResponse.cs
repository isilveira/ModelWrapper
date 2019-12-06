using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQueryResponse : ResponseBase<Category>
    {
        public GetCategoryByIDQueryResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
