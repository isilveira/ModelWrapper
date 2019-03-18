using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Domain.Entities
{
    public class Image
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }

        public string Url { get; set; }
        public string MimeType { get; set; }

        public Product Product { get; set; }
    }
}
