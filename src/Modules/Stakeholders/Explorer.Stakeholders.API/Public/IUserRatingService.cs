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
        // Method to submit a user rating
        Result<string> Create(UserRatingDto ratingDto, string userId);

        // Method to get all ratings (for admin view)
        Result<List<UserRatingDto>> GetAll();
    }
    
}
