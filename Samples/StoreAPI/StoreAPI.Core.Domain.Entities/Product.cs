using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public int Amount { get; set; }
        public decimal Value { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public bool IsVisible { get; set; }

        public List<Image> Images { get; set; }
        public Category Category { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
