using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQueryResponse : WrapResponse<Category>
    {
        public GetCategoriesByFilterQueryResponse(WrapRequest<Category> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
