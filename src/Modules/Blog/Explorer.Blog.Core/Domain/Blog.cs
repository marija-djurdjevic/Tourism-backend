using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
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

    //[Table("Blogs", Schema = "blog")] 

    public class Blogs: Entity
    {
        //[ForeignKey("User")]
        public long AuthorId { get; private set; }
        //public User User { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string? Image { get; private set; }
        public BlogStatus Status { get; private set; }

        public Blogs(long authorId, string title, string description, DateTime creationDate, BlogStatus status, string? image = null)
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
