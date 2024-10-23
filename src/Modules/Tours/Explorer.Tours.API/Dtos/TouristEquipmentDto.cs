using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Explorer.Tours.API.Dtos.TourDto;


namespace Explorer.Tours.API.Dtos;

public class TouristEquipmentDto
{
    public int Id { get; set; }     
    public int EquipmentId { get; set; }
    //public string Name { get; set; }
    //public string? Description { get; set; }
    public int TouristId { get; set; }

    public EquipmentDto? Equipment { get; set; }
}
