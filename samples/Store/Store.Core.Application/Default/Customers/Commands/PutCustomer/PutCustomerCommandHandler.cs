using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Customers;

namespace Store.Core.Application.Default.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandHandler : ApplicationRequestHandler<Customer, PutCustomerCommand, PutCustomerCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutCustomerService PutService { get; set; }
        public PutCustomerCommandHandler(
            IDefaultDbContext context,
            IPutCustomerService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutCustomerCommandResponse> Handle(PutCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutCustomerCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
