using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using static Explorer.Tours.Core.Domain.TourProblems.ProblemComment;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class TourProblem : Entity
    {
        public int TourId { get; private set; }
        public List<Notification>? Notifications { get; private set; }
        public List<ProblemComment>? Comments { get; private set; }
        public ProblemDetails Details { get; private set; }
        public DateTime? Deadline { get; private set; }
        public ProblemStatus Status { get; private set; }



        public TourProblem(int tourId, List<Notification>? notifications, List<ProblemComment>? comments, ProblemDetails details, DateTime? deadline)
        {
            TourId = tourId;
            Notifications = notifications;
            Comments = comments;
            Details = details;
            Deadline = deadline;
        }

        public void AddComment(ProblemComment comment)
        {
            if(Comments == null)
                Comments = new List<ProblemComment>();
            Comments.Add(comment);
            CreateNotification(false, false, 1 );
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

        public void CreateNotification(bool isDeleted, bool deadlineAdded, int receiverId)
        {
            ProblemComment lastComment = Comments.Last();
            Notification lastNotification = Notifications.Last();
            

            int authorId = receiverId;

            if(isDeleted)
            {
                createDeletedNotification(authorId);
            }

            if(deadlineAdded)
            {
                createDeadLineNotification(authorId);
            }
            /*
            if (!lastNotification.IsRead)
            {
                int author = Comments.FirstOrDefault(c => c.Type == ProblemCommentType.FromAuthor).SenderId;
                int touristId = Comments.FirstOrDefault(c => c.Type == ProblemCommentType.FromTourist).SenderId;

                if (lastComment.Type == ProblemCommentType.FromAdmin)
                    createAuthorTouristNotification(lastComment, author, touristId);
                else
                    createAdminNotification(touristId, authorId);
            }
            */
        }

        public void createAdminNotification(int touristId, int authorId)
        {
            string content = $"You have a new comment on the problem by administrator";
            Notifications.Add(new Notification(content, touristId, false));

        }

        public void createAuthorTouristNotification(ProblemComment lastComment, int authorId, int touristId)
        {
            string content = $"You have a new comment on the problem";
            if (lastComment.Type == ProblemCommentType.FromAuthor)
                Notifications.Add(new Notification(content, touristId, false));
            else
                Notifications.Add(new Notification(content, authorId, false));
        }

        private void createDeletedNotification(int recieverId)
        {
            string content = $"The problem has been deleted by administrator";
            Notifications.Add(new Notification(content, recieverId, false));
        }

        private void createDeadLineNotification(int recieverId)
        {
            string content = $"Deadline has been added: {Deadline}";
            Notifications.Add(new Notification(content, recieverId, false));
        }

        public void SetDeadline(DateTime deadline, int receiverId)
        {
            Deadline = deadline;
            CreateNotification(false, true, receiverId);
        }

        public List<Notification> GetUnreadNotifications()
        {
            return Notifications.FindAll(n => !n.IsRead);
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
