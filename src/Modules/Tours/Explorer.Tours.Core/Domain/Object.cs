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
       
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public ObjectCategory Category { get; private set; }

       
        public Object(int id,string name, string description, string image, ObjectCategory category)
        {

            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Category = category;
        }
    }

   
    public enum ObjectCategory
    {
        WC,
        Restaurant,
        Parking,
        Other
    }
}
