using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Entities.Default
{
    public class Image : DomainEntity<int>
    {
        public string Url { get; set; }
        public string MimeType { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Image()
        {
        }
    }
}
