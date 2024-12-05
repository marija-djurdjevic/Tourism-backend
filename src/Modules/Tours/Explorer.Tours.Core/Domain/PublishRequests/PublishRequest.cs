using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.PublishRequests
{
    public class PublishRequest : Entity
    {
        public string? Comment { get; private set; }
        public int AuthorId { get; private set; }
        public int? AdminId { get; private set; }
        public int EntityId { get; private set; }
        public RegistrationRequestStatus Status { get; private set; }
        public RegistrationRequestType Type { get; private set; }
        public PublishRequest() { }

        public PublishRequest(string? comment, int autorId, int? adminId, int entityId, RegistrationRequestStatus status, RegistrationRequestType type)
        {
            Comment = comment;
            AuthorId = autorId;
            AdminId = adminId;
            EntityId = entityId;
            Status = status;
            Type = type;
        }
    }

    public enum RegistrationRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public enum RegistrationRequestType
    {
        Object,
        KeyPoint
    }
}
