using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class Category : DomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RootCategoryId { get; set; }
        public Category RootCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            SubCategories = new HashSet<Category>();
            Products = new HashSet<Product>();
        }
    }
}
