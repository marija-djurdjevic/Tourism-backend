using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ObjectDto

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ObjectCategory Category { get; set; }


        public enum ObjectCategory
        {
            WC,
            Restaurant,
            Parking,
            Other
        }

    }
}
