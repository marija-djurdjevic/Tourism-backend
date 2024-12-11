using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Secrets
{
    public enum StoryStatus
    {
        Pending,
        Accepted,
        Declined
    }

    public class Story : Entity
    {

        public string Content { get; private set; }
        public int AuthorId { get; private set; }
        public int BookId { get; private set; }

        public List<int> ImageIds { get; private set; }

        public StoryStatus StoryStatus { get; private set; }
        public Story()
        {
            ImageIds = new List<int>();
        }

        public Story(string content, int authorId, int bookId, List<int> imageIds, StoryStatus storyStatus)
        {
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Invalid Title.");
            Content = content;
            AuthorId = authorId;
            BookId = bookId;
            ImageIds = imageIds;
            StoryStatus = storyStatus;
        }
    }
}
