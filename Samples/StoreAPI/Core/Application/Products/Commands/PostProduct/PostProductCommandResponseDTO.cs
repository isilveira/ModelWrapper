using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommandResponseDTO
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
    }
}
