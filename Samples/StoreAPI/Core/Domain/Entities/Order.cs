using System;
using System.Collections.Generic;

namespace StoreAPI.Core.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }

        public Customer Customer { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
