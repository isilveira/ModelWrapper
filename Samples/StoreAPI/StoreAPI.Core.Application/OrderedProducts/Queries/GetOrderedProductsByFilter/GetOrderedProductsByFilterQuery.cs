﻿using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQuery : WrapRequest<OrderedProduct>, IRequest<GetOrderedProductsByFilterQueryResponse>
    {
        public GetOrderedProductsByFilterQuery()
        {
            ConfigKeys(x => x.OrderedProductID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
