using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Vote : ValueObject<Vote>
    {
        public int UserId { get; private set; }
        public int BlogId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public bool Value {  get; private set; }

        [JsonConstructor]
        public Vote(int userId, int blogId, DateTime creationDate, bool value)
        {
            UserId = userId;
            BlogId = blogId;
            CreationDate = creationDate;
            Value = value;
        }
        protected override bool EqualsCore(Vote other)
        {
            if (other == null) return false;
            return UserId == other.UserId &&
                BlogId == other.BlogId &&
                CreationDate == other.CreationDate &&
                Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(UserId, BlogId, CreationDate, Value);
        }
    }
}
