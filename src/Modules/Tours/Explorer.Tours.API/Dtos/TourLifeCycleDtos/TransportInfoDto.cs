using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourLifecycleDtos
{
    public class TransportInfoDto
    {
        public enum TransportType
        {
            Walk,
            Car,
            Bicycle
        }
        public int Time { get; set; }
        public double Distance { get; set; }
        public TransportType Transport { get; set; }
    }
}
