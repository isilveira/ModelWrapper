using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommandResponseDTO
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }

        public string Url { get; set; }
        public string MimeType { get; set; }
    }
}
