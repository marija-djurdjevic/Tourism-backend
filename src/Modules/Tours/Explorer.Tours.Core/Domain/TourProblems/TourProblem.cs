using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using static Explorer.Tours.Core.Domain.TourProblems.ProblemComment;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class TourProblem : Entity
    {
        public int TourId { get; private set; }
        public int TouristId { get; private set; }
        public List<ProblemComment>? Comments { get; private set; }
        public ProblemDetails Details { get; private set; }
        public DateTime? Deadline { get; private set; }
        public ProblemStatus Status { get; private set; }

        public TourProblem(int tourId, int touristId, List<ProblemComment>? comments, ProblemDetails details, DateTime? deadline)
        {
            TourId = tourId;
            TouristId = touristId;
            Comments = comments;
            Details = details;
            Deadline = deadline;
        }

        public void AddComment(ProblemComment comment)
        {
            if(Comments == null)
                Comments = new List<ProblemComment>();
            Comments.Add(comment);
        }

        public void ChangeStatus(ProblemStatus status)
        {
            Status = status;
        }

        public void CheckStatus()
        {
            if(Deadline.HasValue && DateTime.Now > Deadline)
            {
                Status = ProblemStatus.Expired;
            }
        }

        public void SetDeadline(DateTime deadline, int receiverId)
        {
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
