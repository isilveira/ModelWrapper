using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Domain.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public List<Order> Orders { get; set; }
    }
}
