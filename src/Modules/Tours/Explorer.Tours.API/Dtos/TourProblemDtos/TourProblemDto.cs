using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourProblemDtos
{
    [JsonDerivedType(typeof(TourProblemDto), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(NotificationDto))]
    [JsonDerivedType(typeof(ProblemDetailsDto))]
    [JsonDerivedType(typeof(ProblemCommentDto))]
    [JsonDerivedType(typeof(ProblemCommentDto))]
    public class TourProblemDto
    {      
        public int Id { get; set; }
        public int TourId { get; set; }

    }

}
