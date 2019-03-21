using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct;
using StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct;
using StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct;
using StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct;
using StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductByID;
using StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter;
using StoreAPI.Resources._Bases;

namespace StoreAPI.Resources
{
    public class OrderedProductsController : ResourceControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetOrderedProductsByFilterQueryResponse>> Get([FromQuery]GetOrderedProductsByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{orderedproductid}")]
        public async Task<ActionResult<GetOrderedProductByIDQueryResponse>> Get([FromRoute] GetOrderedProductByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostOrderedProductCommandResponse>> Post([FromBody] PostOrderedProductCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{orderedproductid}")]
        public async Task<ActionResult<PutOrderedProductCommandResponse>> Put([FromRoute]int orderedProductID, [FromBody]PutOrderedProductCommand request)
        {
            request.Project(x => x.OrderedProductID = orderedProductID);
            return await Send(request);
        }

        [HttpPatch("{orderedproductid}")]
        public async Task<ActionResult<PatchOrderedProductCommandResponse>> Patch([FromRoute]int orderedProductID, [FromBody] PatchOrderedProductCommand request)
        {
            request.Project(x=>x.OrderedProductID = orderedProductID);
            return await Send(request);
        }

        [HttpDelete("{orderedproductid}")]
        public async Task<ActionResult<DeleteOrderedProductCommandResponse>> Delete([FromRoute]DeleteOrderedProductCommand request)
        {
            return await Send(request);
        }
    }
}