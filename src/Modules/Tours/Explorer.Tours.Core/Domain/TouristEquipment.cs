using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class TouristEquipment: Entity
{
    //public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public int TouristId { get; private set; }

    public TouristEquipment(string name, string? description, int touristId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
       // Id = id;
        Name = name;
        Description = description;
        TouristId = touristId;
    }
}
