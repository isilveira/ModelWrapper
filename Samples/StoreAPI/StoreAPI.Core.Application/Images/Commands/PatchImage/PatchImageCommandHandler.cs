﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommandHandler : IRequestHandler<PatchImageCommand, PatchImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchImageCommandResponse> Handle(PatchImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ImageID);
            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == id);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
