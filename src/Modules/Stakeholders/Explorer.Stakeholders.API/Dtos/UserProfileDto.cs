using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserProfileDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageURL { get; set; }
        public string? Biography { get; set; }
        public string? Motto { get; set; }
        public string? Email { get; set; }
        public int? XP { get; set; }
    }

}
