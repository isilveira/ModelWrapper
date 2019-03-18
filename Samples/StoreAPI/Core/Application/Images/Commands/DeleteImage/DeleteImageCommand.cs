using MediatR;

namespace StoreAPI.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest<DeleteImageCommandResponse>
    {
        public int ImageID { get; set; }
        public DeleteImageCommand()
        {
        }
    }
}
