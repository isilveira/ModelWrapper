using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Samples;
using Store.Core.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ModelWrapper.Extensions.Patch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Samples.Commands.PatchSample
{
    public class PatchSampleCommandHandler : ApplicationRequestHandler<Sample, PatchSampleCommand, PatchSampleCommandResponse>
    {
        private IStringLocalizer MessagesLocalizer { get; set; }
        private IStringLocalizer EntitiesDefaultLocalizer { get; set; }
        public IDefaultDbContext Context { get; set; }
        private IPatchSampleService PatchService { get; set; }
        public PatchSampleCommandHandler(
            IStringLocalizer<Messages> messagesLocalizer,
            IStringLocalizer<EntitiesDefault> entitiesDefaultLocalizer,
            IDefaultDbContext context,
            IPatchSampleService patchService)
        {
            MessagesLocalizer = messagesLocalizer;
            EntitiesDefaultLocalizer = entitiesDefaultLocalizer;
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchSampleCommandResponse> Handle(PatchSampleCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Samples.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception(string.Format(MessagesLocalizer["{0} not found!"], EntitiesDefaultLocalizer[nameof(Sample)]));
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchSampleCommandResponse(request, data, MessagesLocalizer["Successful operation!"], 1);
        }
    }
}
