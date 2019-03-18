using MediatR;

namespace StoreAPI.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommand : IRequest<PutCustomerCommandResponse>
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
