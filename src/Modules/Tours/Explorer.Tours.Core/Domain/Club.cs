using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Club : Entity
    {
        public int OwnerId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ImageId {  get; private set; }

        public Club(string name, string description, int imageId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Desc.");
            //if (string.IsNullOrWhiteSpace(imageId)) throw new ArgumentException("Invalid Image.");
            Name = name;
            Description = description;
            ImageId = imageId;
        }
    }
}
