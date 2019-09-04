using MediatR;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQuery : IRequest<GetOrderedProductByIDQueryResponse>
    {
        public int OrderedProductID { get; set; }
    }
}
