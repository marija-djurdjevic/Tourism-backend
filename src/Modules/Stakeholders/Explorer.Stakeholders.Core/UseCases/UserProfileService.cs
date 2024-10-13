using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserProfileService : BaseService<UserProfileDto, UserProfile>,IUserProfileService
    {
        public UserProfileService(IMapper mapper) : base(mapper)
        {
        }

        public Result GetUserProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public Result UpdateUserProfile(UserProfileDto userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
