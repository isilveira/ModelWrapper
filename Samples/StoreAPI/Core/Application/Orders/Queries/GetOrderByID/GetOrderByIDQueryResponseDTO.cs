using System;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQueryResponseDTO
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
