using MediatR;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommand : IRequest<PostOrderCommandResponse>
    {
        public int CustomerID { get; set; }
    }
}
