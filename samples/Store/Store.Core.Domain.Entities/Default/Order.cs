using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class Order : DomainEntity
    {
        public int Id { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderedProduct> OrderedProducts { get; set; }

        public Order()
        {
            OrderedProducts = new HashSet<OrderedProduct>();
        }
    }
}
