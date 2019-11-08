using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Application.Orders.Commands.DeleteOrder;
using Store.Core.Application.Orders.Commands.PatchOrder;
using Store.Core.Application.Orders.Commands.PostOrder;
using Store.Core.Application.Orders.Commands.PutOrder;
using Store.Core.Application.Orders.Queries.GetOrderByID;
using Store.Core.Application.Orders.Queries.GetOrdersByFilter;
using Store.Presentations.StoreAPI.Resources.Bases;

namespace Store.Presentations.StoreAPI.Resources
{
    public class OrdersController : MediatorControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetOrdersByFilterQueryResponse>> Get([FromQuery]GetOrdersByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult<GetOrderByIDQueryResponse>> Get([FromRoute] GetOrderByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostOrderCommandResponse>> Post([FromBody] PostOrderCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{orderid}")]
        public async Task<ActionResult<PutOrderCommandResponse>> Put([FromRoute]int orderID, [FromBody]PutOrderCommand request)
        {
            request.Project(x => x.OrderID = orderID);
            return await Send(request);
        }

        [HttpPatch("{orderid}")]
        public async Task<ActionResult<PatchOrderCommandResponse>> Patch([FromRoute]int orderID, [FromBody] PatchOrderCommand request)
        {
            request.Project(x=>x.OrderID = orderID);
            return await Send(request);
        }

        [HttpDelete("{orderid}")]
        public async Task<ActionResult<DeleteOrderCommandResponse>> Delete([FromRoute]DeleteOrderCommand request)
        {
            return await Send(request);
        }
    }
}