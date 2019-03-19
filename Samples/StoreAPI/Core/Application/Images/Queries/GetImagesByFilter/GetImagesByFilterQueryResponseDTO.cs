using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryResponseDTO
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }

        public string Url { get; set; }
        public string MimeType { get; set; }
    }
}
