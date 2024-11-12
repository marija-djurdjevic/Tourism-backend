using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<bool> Exists(string username);
        Result<UserDto> GetActiveByName(string username);
        Result<UserDto> Create(UserDto userDto);
        Result<long> GetPersonId(long userId);
        Result<PersonDto> GetPersonByUserId(long userId);
        Result<LocationDto> SetUserLocation(long userId, float longitude, float latitude);
        Result<LocationDto> GetUserLocation(long userId);
        Result<string> GetUsernameById(long userId);
    }
}
