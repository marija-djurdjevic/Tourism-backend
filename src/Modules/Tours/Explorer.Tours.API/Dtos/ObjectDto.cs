using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ObjectDto

    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ImageId { get; set; }
        public ObjectCategory Category { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public ObjectStatus Status { get; set; }
        public enum ObjectStatus
        {
            Pending,
            Private,
            Public,
            Rejected
        }

        public enum ObjectCategory
        {
            WC,
            Restaurant,
            Parking,
            Other
        }

    }
}
