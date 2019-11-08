using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommandResponse : WrapResponse<Image>
    {
        public PatchImageCommandResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
