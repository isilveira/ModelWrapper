using Microsoft.AspNetCore.Mvc;
using StoreAPI.Core.Application.Products.Commands.DeleteProduct;
using StoreAPI.Core.Application.Products.Commands.PatchProduct;
using StoreAPI.Core.Application.Products.Commands.PostProduct;
using StoreAPI.Core.Application.Products.Commands.PutProduct;
using StoreAPI.Core.Application.Products.Queries.GetProductByID;
using StoreAPI.Core.Application.Products.Queries.GetProductsByFilter;
using StoreAPI.Resources._Bases;
using System.Threading.Tasks;

namespace StoreAPI.Resources
{
    public class ProductsController : ResourceControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetProductsByFilterQueryResponse>> Get([FromQuery]GetProductsByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{productid}")]
        public async Task<ActionResult<GetProductByIDQueryResponse>> Get([FromRoute] GetProductByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostProductCommandResponse>> Post([FromBody] PostProductCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{productid}")]
        public async Task<ActionResult<PutProductCommandResponse>> Put([FromRoute]int productid, [FromBody]PutProductCommand request)
        {
            request.Project(x => x.ProductID = productid);
            return await Send(request);
        }

        [HttpPatch("{productid}")]
        public async Task<ActionResult<PatchProductCommandResponse>> Patch([FromRoute]int productid, [FromBody] PatchProductCommand request)
        {
            request.Project(x => x.ProductID = productid);
            return await Send(request);
        }

        [HttpDelete("{productid}")]
        public async Task<ActionResult<DeleteProductCommandResponse>> Delete([FromRoute]DeleteProductCommand request)
        {
            return await Send(request);
        }
    }
}
