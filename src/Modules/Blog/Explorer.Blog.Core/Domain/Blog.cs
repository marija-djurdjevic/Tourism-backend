using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        //[ForeignKey(nameof(AuthorId))]
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string? Image { get; private set; }
        public BlogStatus Status { get; private set; }

        public Blogs(int authorId, string title, string description, DateTime creationDate, BlogStatus status, string? image = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            AuthorId = authorId;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Image = image;
            Status = status;
        }
    }
}
