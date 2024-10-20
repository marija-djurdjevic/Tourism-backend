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
        public string Image {  get; private set; }

        public Club(string name, string description, string image)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Desc.");
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Invalid Image.");
            Name = name;
            Description = description;
            Image = image;
        }
    }
}
