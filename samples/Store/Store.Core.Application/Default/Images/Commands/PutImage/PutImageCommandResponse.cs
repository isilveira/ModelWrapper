using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.PutImage
{
    public class PutImageCommandResponse : ApplicationResponse<Image>
    {
        public PutImageCommandResponse(WrapRequest<Image> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
