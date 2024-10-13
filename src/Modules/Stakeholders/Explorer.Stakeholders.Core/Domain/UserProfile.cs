using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserProfile : Entity
    {
        public long UserId  { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? ImageURL { get; private set; }
        public string? Biography { get; private set; }
        public string? Motto { get; private set; }


        public UserProfile(long userId,string firstName, string lastName, string imageURL, string biography, string motto)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ImageURL = imageURL;
            Biography = biography;
            Motto = motto;

        }

    }
}
