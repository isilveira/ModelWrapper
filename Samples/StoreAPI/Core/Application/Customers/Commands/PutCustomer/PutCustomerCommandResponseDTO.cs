namespace StoreAPI.Core.Application.Customers.Commands.PutCustomer
{
    public class PutCustomerCommandResponseDTO
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
