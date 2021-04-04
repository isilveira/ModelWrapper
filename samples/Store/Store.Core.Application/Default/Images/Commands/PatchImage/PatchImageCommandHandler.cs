using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Images;

namespace Store.Core.Application.Default.Images.Commands.PatchImage
{
    public class PatchImageCommandHandler : ApplicationRequestHandler<Image, PatchImageCommand, PatchImageCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchImageService PatchService { get; set; }
        public PatchImageCommandHandler(
            IDefaultDbContext context,
            IPatchImageService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchImageCommandResponse> Handle(PatchImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Images.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
