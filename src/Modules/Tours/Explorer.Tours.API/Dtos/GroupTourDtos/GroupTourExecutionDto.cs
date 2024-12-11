using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.GroupTourDtos
{
    public class GroupTourExecutionDto
    {
        public int GroupTourId { get; set; }
        public int TouristId { get; set; }
        public bool IsFinished { get; set; }


    }
}
