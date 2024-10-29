using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Coordinates : ValueObject<Coordinates>
    {
        public string Content { get; }
        public int RecieverId { get; }
        public bool IsRead { get; private set; }

        [JsonConstructor]
        public Coordinates(string content, int recieverId, bool isRead)
        {
            Content = content;
            RecieverId = recieverId;
            IsRead = isRead;
        }
        protected override bool EqualsCore(Coordinates other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
