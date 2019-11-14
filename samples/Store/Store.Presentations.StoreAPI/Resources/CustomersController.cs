using Microsoft.AspNetCore.Mvc;
using Store.Core.Application.Customers.Commands.DeleteCustomer;
using Store.Core.Application.Customers.Commands.PatchCustomer;
using Store.Core.Application.Customers.Commands.PostCustomer;
using Store.Core.Application.Customers.Commands.PutCustomer;
using Store.Core.Application.Customers.Queries.GetCustomerByID;
using Store.Core.Application.Customers.Queries.GetCustomersByFilter;
using Store.Presentations.StoreAPI.Resources.Bases;
using System.Threading.Tasks;

namespace Store.Presentations.StoreAPI.Resources
{
    public class PostCustomerVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class CustomersController : MediatorControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCustomersByFilterQueryResponse>> Get([FromQuery]GetCustomersByFilterQuery request)
        {
            return await Send(request);
        }

        [HttpGet("{customerid}")]
        public async Task<ActionResult<GetCustomerByIDQueryResponse>> Get([FromRoute] GetCustomerByIDQuery request)
        {
            return await Send(request);
        }
        [HttpPost]
        public async Task<ActionResult<PostCustomerCommandResponse>> Post([FromBody]PostCustomerCommand request)
        {
            return await Send(request);
        }

        [HttpPut("{customerid}")]
        public async Task<ActionResult<PutCustomerCommandResponse>> Put([FromRoute]int customerID, [FromBody]PutCustomerCommand request)
        {
            request.Project(x => x.CustomerID = customerID);
            return await Send(request);
        }

        [HttpPatch("{customerid}")]
        public async Task<ActionResult<PatchCustomerCommandResponse>> Patch([FromRoute]int customerID, [FromBody] PatchCustomerCommand request)
        {
            request.Project(x => x.CustomerID = customerID);
            return await Send(request);
        }

        [HttpDelete("{customerid}")]
        public async Task<ActionResult<DeleteCustomerCommandResponse>> Delete([FromRoute]DeleteCustomerCommand request)
        {
            return await Send(request);
        }
    }
}