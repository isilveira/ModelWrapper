namespace StoreAPI.Core.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandResponseDTO
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
