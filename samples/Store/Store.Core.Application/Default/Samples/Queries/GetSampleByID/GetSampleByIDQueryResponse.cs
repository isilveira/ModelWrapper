using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Samples.Queries.GetSampleByID
{
    public class GetSampleByIDQueryResponse : ApplicationResponse<Sample>
    {
        public GetSampleByIDQueryResponse(WrapRequest<Sample> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
