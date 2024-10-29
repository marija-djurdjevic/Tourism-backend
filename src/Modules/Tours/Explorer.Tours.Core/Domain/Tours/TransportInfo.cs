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
    public class TransportInfo : ValueObject<TransportInfo>
    {
        public string Content { get; }
        public int RecieverId { get; }
        public bool IsRead { get; private set; }

        protected override bool EqualsCore(TransportInfo other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        [JsonConstructor]
        public TransportInfo(string content, int recieverId, bool isRead)
        {
            Content = content;
            RecieverId = recieverId;
            IsRead = isRead;
        }
    }
}
