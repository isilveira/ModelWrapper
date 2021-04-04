using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.PatchImage
{
    public class PatchImageCommandResponse : ApplicationResponse<Image>
    {
        public PatchImageCommandResponse(WrapRequest<Image> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
