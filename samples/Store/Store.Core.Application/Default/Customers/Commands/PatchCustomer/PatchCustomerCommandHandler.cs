using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Customers;

namespace Store.Core.Application.Default.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommandHandler : ApplicationRequestHandler<Customer, PatchCustomerCommand, PatchCustomerCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchCustomerService PatchService { get; set; }
        public PatchCustomerCommandHandler(
            IDefaultDbContext context,
            IPatchCustomerService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchCustomerCommandResponse> Handle(PatchCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchCustomerCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
