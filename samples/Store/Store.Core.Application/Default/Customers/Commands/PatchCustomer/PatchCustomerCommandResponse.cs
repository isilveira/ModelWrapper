using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommandResponse : ApplicationResponse<Customer>
    {
        public PatchCustomerCommandResponse(WrapRequest<Customer> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
