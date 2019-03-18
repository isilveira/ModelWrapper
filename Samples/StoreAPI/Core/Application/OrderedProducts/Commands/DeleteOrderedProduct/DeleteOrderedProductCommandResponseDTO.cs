using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandResponseDTO
    {
        public int OrderedProductID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int Amount { get; set; }
        public decimal Value { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
