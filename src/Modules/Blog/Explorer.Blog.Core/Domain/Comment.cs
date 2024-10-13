using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity
    {
        public int AuthorId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Text { get; private set; }
        public DateTime EditDate { get; private set; }
        public int BlogId { get; private set; }

        public Comment(int authorId, string text, int blogId)
        {
            AuthorId = authorId;
            CreationDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid Name.");
            EditDate = DateTime.Now;
            BlogId = blogId;
        }
    }
}
