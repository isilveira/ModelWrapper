using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, DeleteImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteImageCommandResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ImageID);

            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == id);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            Context.Images.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
