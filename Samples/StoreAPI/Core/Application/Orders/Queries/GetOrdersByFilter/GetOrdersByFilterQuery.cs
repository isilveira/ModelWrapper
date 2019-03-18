using MediatR;
using System;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQuery : IRequest<GetOrdersByFilterQueryResponse>
    {
        public int? CustomerID { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
