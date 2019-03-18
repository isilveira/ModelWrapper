using MediatR;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQuery : IRequest<GetOrderByIDQueryResponse>
    {
        public int OrderID { get; set; }
    }
}
