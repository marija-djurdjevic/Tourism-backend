using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserRatingService
    {
        Result<string> Create(UserRatingDto ratingDto, string userId);
        Result<List<UserRatingDto>> GetAll();
    }
    
}
