using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQueryResponse : ApplicationResponse<Category>
    {
        public GetCategoriesByFilterQueryResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
