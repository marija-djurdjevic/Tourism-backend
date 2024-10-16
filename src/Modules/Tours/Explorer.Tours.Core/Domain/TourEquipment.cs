using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class TourEquipment : Entity
    {
        public int TourId { get; private set; }
        public int EquipmentId { get; private set; }

        public TourEquipment(int tourId, int equipmentId)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
        }
    }
}
