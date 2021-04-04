using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class OrderedProduct : DomainEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public OrderedProduct()
        {
        }
    }
}
