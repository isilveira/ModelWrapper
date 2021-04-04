using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Customers;

namespace Store.Core.Application.Default.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : ApplicationRequestHandler<Customer, DeleteCustomerCommand, DeleteCustomerCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteCustomerService DeleteService { get; set; }
        public DeleteCustomerCommandHandler(
            IDefaultDbContext context,
            IDeleteCustomerService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Customer not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteCustomerCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
