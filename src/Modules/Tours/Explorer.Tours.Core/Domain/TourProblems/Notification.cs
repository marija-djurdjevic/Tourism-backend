using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class Notification : Entity
    {
        public string Content { get; private set; }
        public NotificationType Type {get; private set; }   
        public int ReferenceId { get; private set;  } //U zavisnosti koji je tip notifikacije ovde ce biti id komentara, problema itd
        public int RecieverId {get; private set; }
        public bool IsRead { get; private set; }
          
        [JsonConstructor]
        public Notification(string content, int referenceId, NotificationType type, int recieverId, bool isRead)
        {
            Content = content;
            Type = type;
            RecieverId = recieverId;
            ReferenceId = referenceId;
            IsRead = isRead;
        }
    }

    public enum NotificationType
    {
        TourProblemComment,
        TourRefundComment
        //Ko bude koristio notifikacije neka sebi doda tip koji mu treba
    }
}
