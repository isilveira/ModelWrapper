using MediatR;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommand : IRequest<PostImageCommandResponse>
    {
        public int ProductID { get; set; }

        public string Url { get; set; }
        public string MimeType { get; set; }

        public PostImageCommand()
        {
        }
    }
}
