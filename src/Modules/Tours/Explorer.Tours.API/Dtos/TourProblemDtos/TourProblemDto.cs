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
        public int TouristId { get; set; }  
        public ProblemDetailsDto Details { get; set; }
        public List<ProblemCommentDto>? Comments { get; set; }
        public ProblemStatus Status { get; set; }
        public DateTime? Deadline { get; set; }

        public TourProblemDto(int tourId, int touristId, ProblemDetailsDto details, List<ProblemCommentDto>? comments, ProblemStatus status, DateTime? deadline)
        {
            TourId = tourId;
            TouristId = touristId;
            Details = details;
            Comments = comments;
            Status = status;
            Deadline = deadline;
        }
    }
    public enum ProblemStatus
    {
        Pending,
        Solved,
        Closed,
        Expired
    }
}
