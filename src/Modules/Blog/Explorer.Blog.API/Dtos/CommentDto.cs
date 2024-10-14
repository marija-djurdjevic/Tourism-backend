using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        //public int BlogId { get; set; }
    }
}
