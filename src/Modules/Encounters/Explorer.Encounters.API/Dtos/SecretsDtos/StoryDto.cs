using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.SecretsDtos
{
    public enum StoryStatus
    {
        Pending,
        Accepted,
        Declined
    }
    public class StoryDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        public string Title { get; set; }
        public int ImageId { get; set; }

        public StoryStatus StoryStatus { get; set; }
    }
}
