using MediatR;

namespace StoreAPI.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<DeleteOrderCommandResponse>
    {
        public int OrderID { get; set; }
    }
}
