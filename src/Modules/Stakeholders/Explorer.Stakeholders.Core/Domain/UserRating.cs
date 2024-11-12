using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserRating : Entity
    {
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long UserId { get; private set; }
        public string Username { get; private set; }
        public UserRating() { }

        public UserRating(int rating, string? comment, long userId, string username)
        {
            Rating = rating;
            Comment = comment;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            Username = username;
            Validate();
        }

        private void Validate()
        {
            if (Rating < 1 || Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }
        }
    }
}