using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class ProblemComment : ValueObject<ProblemComment>
    {

        public string Content { get;}
        public UserRole SenderRole { get; }
        public int SenderId { get; }
        public DateTime SentTime { get; }

        [JsonConstructor]
        public ProblemComment(string content, UserRole role, DateTime sentTime, int senderId)
        {
            Content = content;
            SenderRole = role;
            SentTime = sentTime;
            SenderId = senderId;  
        }

        protected override bool EqualsCore(ProblemComment other)
        {
            if (other == null) return false;
            return Content == other.Content &&
                SenderId == other.SenderId &&
                SenderRole == other.SenderRole &&
                SentTime == other.SentTime;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Content, SenderRole, SenderId, SentTime);
        }   
    }
}
