using MediatR;

namespace StoreAPI.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommand : IRequest<PatchCustomerCommandResponse>
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
