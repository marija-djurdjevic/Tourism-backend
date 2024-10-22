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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? ImageURL { get; private set; }
        public string? Biography { get; private set; }
        public string? Motto { get; private set; }


        public UserProfile(long id,string firstName, string lastName, string imageURL, string biography, string motto)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ImageURL = imageURL;
            Biography = biography;
            Motto = motto;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentException("Invalid First Name");
            if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentException("Invalid Last Name");
        }

    }
}
