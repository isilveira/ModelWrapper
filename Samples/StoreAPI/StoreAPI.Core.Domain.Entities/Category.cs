using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int? RootCategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Category RootCategory { get; set; }
        public List<Category> LeafCategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
