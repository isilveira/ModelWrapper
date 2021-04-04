using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class Product : DomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public int Amount { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisteredAt { get; set; }
        public bool IsVisible { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<OrderedProduct> OrderedProducts { get; set; }
        public Product()
        {
            Images = new HashSet<Image>();
            OrderedProducts = new HashSet<OrderedProduct>();
        }
    }
}
