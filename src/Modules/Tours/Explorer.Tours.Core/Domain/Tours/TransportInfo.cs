using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public enum TransportType
    {
        Walk,
        Car,
        Bicycle
    }
    public class TransportInfo : ValueObject<TransportInfo>
    {
        public int Time { get;  set; }
        public double Distance { get;  set; }
        public TransportType Transport {  get; private set; }

        protected override bool EqualsCore(TransportInfo other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        [JsonConstructor]
        public TransportInfo(int time, double distance, TransportType transport)
        {
            Time = time;
            Distance = distance;
            Transport = transport;
        }
    }
}
