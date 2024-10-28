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

        public string SenderId { get; }

        public DateTime SentTime { get; }

        [JsonConstructor]
        public ProblemComment(string text, UserRole role, DateTime sentTime, string senderId)
        {
            Content = text;
            SenderRole = role;
            SentTime = sentTime;
            SenderId = senderId;    
        }

        protected override bool EqualsCore(ProblemComment other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

       
    }
}
