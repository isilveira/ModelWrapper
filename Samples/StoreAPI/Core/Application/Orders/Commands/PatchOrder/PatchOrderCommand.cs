using MediatR;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : IRequest<PatchOrderCommandResponse>
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }

        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
