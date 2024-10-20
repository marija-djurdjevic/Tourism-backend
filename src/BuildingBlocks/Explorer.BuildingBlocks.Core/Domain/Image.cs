using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public class Image: Entity
    {
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        public Image(byte[] imageData, string contentType)
        {
            if (imageData == null || imageData.Length == 0) throw new ArgumentException("Invalid Image Data.");
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentException("Invalid Content Type.");
            ImageData = imageData;
            ContentType = contentType;
        }
    }
}
