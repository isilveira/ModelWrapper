using MediatR;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQuery : IRequest<GetCustomerByIDQueryResponse>
    {
        public int CustomerID { get; set; }
    }
}
