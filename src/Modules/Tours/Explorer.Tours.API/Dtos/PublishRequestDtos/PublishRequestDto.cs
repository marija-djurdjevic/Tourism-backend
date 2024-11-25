using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.PublishRequestDtos
{
    public class PublishRequestDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public RegistrationRequestStatus Status { get; set; }
        public RegistrationRequestType Type { get; set; }
        public int AuthorId { get; set; }
        public int AdminId { get; set; }
        public int EntityId { get; set; }

        public PublishRequestDto(string comment, int authorId, int adminId, int entityId, RegistrationRequestStatus status, RegistrationRequestType type)
        {
            Comment = comment;
            AuthorId = authorId;
            AdminId = adminId;
            EntityId = entityId; // Correct parameter name
            Status = status;
            Type = type;
        }

        public PublishRequestDto() { }
       
    

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
}
