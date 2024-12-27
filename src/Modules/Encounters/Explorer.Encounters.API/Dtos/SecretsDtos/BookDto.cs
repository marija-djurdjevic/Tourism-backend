using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.SecretsDtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Title { get; set; }
        public int PageNum { get; set; }
        public string BookColour { get; set; }
    }
}
