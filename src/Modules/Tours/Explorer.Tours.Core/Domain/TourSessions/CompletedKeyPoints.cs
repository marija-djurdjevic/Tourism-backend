using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourSessions
{
    public class CompletedKeyPoints: ValueObject<CompletedKeyPoints>
    {
        public int KeyPointId { get; private set; }
        public DateTime CompletedAt { get; private set; }


        [JsonConstructor]
        public CompletedKeyPoints(int keyPointId, DateTime completedAt)
        {
            KeyPointId = keyPointId;
            CompletedAt = completedAt;
        }

        protected override bool EqualsCore(CompletedKeyPoints other)
        {
            return KeyPointId == other.KeyPointId &&
                   CompletedAt == other.CompletedAt;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(KeyPointId, CompletedAt);
        }
    }
}
