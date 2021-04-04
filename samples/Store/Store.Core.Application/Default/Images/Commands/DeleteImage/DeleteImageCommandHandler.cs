using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Images;

namespace Store.Core.Application.Default.Images.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : ApplicationRequestHandler<Image, DeleteImageCommand, DeleteImageCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteImageService DeleteService { get; set; }
        public DeleteImageCommandHandler(
            IDefaultDbContext context,
            IDeleteImageService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteImageCommandResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Images.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Image not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteImageCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
