using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Queries.GetImageByID
{
    public class GetImageByIDQueryResponse : ApplicationResponse<Image>
    {
        public GetImageByIDQueryResponse(WrapRequest<Image> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
