using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourProblemDtos
{
    public class TourProblemDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public List<NotificationDto>? Notifications { get; set; }
        public ProblemDetailsDto Details { get; set; }
        public List<ProblemCommentDto>? Comments { get; set; }
        public ProblemStatus Status { get; set; }
        public DateTime? Deadline { get; set; }
    }
    public enum ProblemStatus
    {
        Pending,
        Solved,
        Closed,
        Expired
    }
}
