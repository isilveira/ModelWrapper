using MediatR;

namespace StoreAPI.Core.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerCommandResponse>
    {
        public int CustomerID { get; set; }
    }
}
