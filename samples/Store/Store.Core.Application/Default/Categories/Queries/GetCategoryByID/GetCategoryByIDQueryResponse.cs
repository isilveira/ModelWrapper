using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQueryResponse : ApplicationResponse<Category>
    {
        public GetCategoryByIDQueryResponse(WrapRequest<Category> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
