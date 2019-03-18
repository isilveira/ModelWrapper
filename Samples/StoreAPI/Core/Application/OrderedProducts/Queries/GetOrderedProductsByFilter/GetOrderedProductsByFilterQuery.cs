using MediatR;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQuery : IRequest<GetOrderedProductsByFilterQueryResponse>
    {
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }

        public int? Amount { get; set; }
        public decimal? Value { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}
