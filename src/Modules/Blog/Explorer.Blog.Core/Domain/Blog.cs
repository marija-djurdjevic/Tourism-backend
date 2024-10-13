using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed,
    }
    public class Blogs: Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<string>? Images { get; private set; }
        public BlogStatus Status { get; private set; }

        public Blogs(string title, string description, List<string>? images = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            Title = title;
            Description = description;
            CreationDate = DateTime.Now;
            Images = images;
            Status = BlogStatus.Draft;
        }
    }
}
