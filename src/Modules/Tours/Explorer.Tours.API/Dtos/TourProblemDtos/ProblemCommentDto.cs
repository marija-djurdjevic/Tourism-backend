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
        public UserRole SenderRole { get; }
        public string SenderId { get; set; }
        public DateTime SentTime { get; set; }
    }
}
