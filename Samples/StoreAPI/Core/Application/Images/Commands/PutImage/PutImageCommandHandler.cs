using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PutImage
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
            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == request.ImageID);

            if (data == null)
                throw new Exception("Image not found!");

            data.MimeType = request.MimeType;
            data.ProductID = request.ProductID;
            data.Url = request.Url;

            await Context.SaveChangesAsync();

            return new PutImageCommandResponse
            {
                Message = "Successful operation!",
                Request = request,
                Data = new PutImageCommandResponseDTO
                {
                    ImageID = data.ImageID,
                    ProductID = data.ProductID,
                    MimeType = data.MimeType,
                    Url = data.Url                    
                }
            };
        }
    }
}
