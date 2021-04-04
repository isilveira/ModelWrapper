using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Images;

namespace Store.Core.Application.Default.Images.Commands.PutImage
{
    public class PutImageCommandHandler : ApplicationRequestHandler<Image, PutImageCommand, PutImageCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutImageService PutService { get; set; }
        public PutImageCommandHandler(
            IDefaultDbContext context,
            IPutImageService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutImageCommandResponse> Handle(PutImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.Images.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
