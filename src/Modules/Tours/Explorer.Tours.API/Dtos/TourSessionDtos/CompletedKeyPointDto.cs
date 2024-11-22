using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourSessionDtos
{
    public class CompletedKeyPointDto
    {
        public int KeyPointId { get; set; }
        public DateTime CompletedAt { get; set; }

        public CompletedKeyPointDto(int keyPointId, DateTime completedAt)
        {
            KeyPointId = keyPointId;
            CompletedAt = completedAt;
        }
    }
}