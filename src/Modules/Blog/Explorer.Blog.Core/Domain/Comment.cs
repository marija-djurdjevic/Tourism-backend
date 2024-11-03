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
    public class Comment : Entity
    {
        public long AuthorId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Text { get; private set; }
        public DateTime EditDate { get; private set; }
        public long BlogId { get; private set; }
        public string Username { get; private set; }

        public Comment(long authorId, string text, DateTime creationDate, DateTime editDate, long blogId, string username)
        {
            AuthorId = authorId;
            CreationDate = creationDate;
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid comment.");
            Text = text;
            EditDate = editDate;
            BlogId = blogId;
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Invalid comment.");
            Username = username;
        }
    }
}
