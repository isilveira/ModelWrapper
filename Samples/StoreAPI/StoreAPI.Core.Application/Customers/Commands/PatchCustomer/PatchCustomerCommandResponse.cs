using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomerCommandResponse : CommandResponse<Dictionary<string,object>, PatchCustomerCommandResponseDTO>
    {
    }
}
