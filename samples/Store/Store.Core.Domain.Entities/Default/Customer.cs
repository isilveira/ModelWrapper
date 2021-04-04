using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class Customer : DomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
}
