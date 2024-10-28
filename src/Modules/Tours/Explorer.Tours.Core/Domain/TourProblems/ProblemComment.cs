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
        public ProblemCommentType Type { get; }
        public int SenderId { get; }
        public DateTime SentTime { get; }

        [JsonConstructor]
        public ProblemComment(string content, ProblemCommentType type, DateTime sentTime, int senderId)
        {
            Content = content;
            Type = type;
            SentTime = sentTime;
            SenderId = senderId;  
        }

        protected override bool EqualsCore(ProblemComment other)
        {
            if (other == null) return false;
            return Content == other.Content &&
                SenderId == other.SenderId &&
                Type == other.Type &&
                SentTime == other.SentTime;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Content, Type, SenderId, SentTime);
        }   
    }

    public enum ProblemCommentType
    {
        FromTourist,
        FromAuthor,
        FromAdmin
    }
}
