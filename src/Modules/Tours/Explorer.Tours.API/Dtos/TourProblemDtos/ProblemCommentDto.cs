using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourProblemDtos
{
    public class ProblemCommentDto
    {
        public string Content { get; set; }
        public ProblemCommentType Type { get; set; }
        public int SenderId { get; set; }
        public DateTime SentTime { get; set; }
    }

    public enum ProblemCommentType
    {
        FromTourist,
        FromAuthor,
        FromAdmin
    }
}
