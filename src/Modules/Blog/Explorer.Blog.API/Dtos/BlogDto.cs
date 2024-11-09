using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogDto
    {
        public enum BlogStatus
        {
            Draft,
            Published,
            Canceled,
            Completed,
            Active,
            Famous
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public List<VoteDto> Votes { get; set; }
        //public List<CommentDto> Comments { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
        public BlogStatus Status { get; set; }
    }
}
