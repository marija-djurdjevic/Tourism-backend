using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourProblemDtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public int ReferenceId { get; set; }
        public int RecieverId { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImagePath { get; set; }

        public NotificationDto(string content, NotificationType type, int referenceId, int recieverId, bool isRead)
        {
            Content = content;
            Type = type;
            ReferenceId = referenceId;
            RecieverId = recieverId;
            IsRead = isRead;
        }
    }
}

public enum NotificationType
{
    TourProblemComment,
    PublicRequest,
    TourRefundComment,

    Achievement,
    GroupTourCancelationComment

    //Ko bude koristio notifikacije neka sebi doda tip koji mu treba
}