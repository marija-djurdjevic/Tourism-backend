using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourProblemDtos
{
    public class NotificationDto
    {
        public string Content { get; set; }
        public int RecieverId { get; set; }

        public bool IsRead { get; set; }

    }
}
