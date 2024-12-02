using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class Object : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ImageId { get; private set; }
        public ObjectCategory Category { get; private set; }
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }

        public ObjectStatus Status { get; private set; }

        public Object(string name, string description, int imageId, ObjectCategory category, float longitude, float latitude)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            ImageId = imageId;
            Category = category;
            this.Longitude = longitude;
            this.Latitude = latitude;
            Status = ObjectStatus.Pending;
        }

        public Object(string name, string description, int imageId, ObjectCategory category, float longitude, float latitude, ObjectStatus type)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            ImageId = imageId;
            Category = category;
            this.Longitude = longitude;
            this.Latitude = latitude;
            Status = type;
        }

        public void UpdateObjectStatus(ObjectStatus status)
        {
            Status = status;
        }
    }

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
