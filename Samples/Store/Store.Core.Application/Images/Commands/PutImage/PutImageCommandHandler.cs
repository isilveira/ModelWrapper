using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommandHandler : IRequestHandler<PutImageCommand, PutImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutImageCommandResponse> Handle(PutImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ImageID);
            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == id);

            if (data == null)
                throw new Exception("Image not found!");

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
