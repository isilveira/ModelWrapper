namespace StoreAPI.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryResponseDTO
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
