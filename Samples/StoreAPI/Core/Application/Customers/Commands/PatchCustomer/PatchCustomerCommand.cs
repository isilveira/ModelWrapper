using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommand : Wrap<Customer,int>, IRequest<PatchCustomerCommandResponse>
    {
        public PatchCustomerCommand()
        {
            SuppressProperty(x => x.CustomerID);
        }
    }
}
