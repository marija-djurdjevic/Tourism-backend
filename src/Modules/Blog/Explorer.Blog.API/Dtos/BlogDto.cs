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
            Completed
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> Images { get; set; }
        public BlogStatus Status { get; set; }
    }
}
