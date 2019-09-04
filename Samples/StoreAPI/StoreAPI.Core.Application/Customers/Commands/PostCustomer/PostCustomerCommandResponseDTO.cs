namespace StoreAPI.Core.Application.Customers.Commands.PostCustomer
{
    public class PostCustomerCommandResponseDTO
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
