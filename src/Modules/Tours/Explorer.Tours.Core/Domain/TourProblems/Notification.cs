using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class Notification : ValueObject<Notification>
    {
        public string Content { get; }
        public int RecieverId   {get;  }

        public bool IsRead { get; }
           
        [JsonConstructor]
        public Notification(string content, int recieverId, bool isRead) 
        {
            Content = content;
            RecieverId = recieverId;
            IsRead = isRead;
        }
        protected override bool EqualsCore(Notification other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
