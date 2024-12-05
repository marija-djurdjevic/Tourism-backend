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
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Value { get; set; }

        [JsonConstructor]
        public Vote(int authorId, DateTime creationDate, bool value)
        {
            AuthorId = authorId;
            CreationDate = creationDate;
            Value = value;
        }
        protected override bool EqualsCore(Vote other)
        {
            if (other == null) return false;
            return AuthorId == other.AuthorId &&
                CreationDate == other.CreationDate &&
                Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(AuthorId, CreationDate, Value);
        }
    }
}