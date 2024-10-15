using Explorer.Stakeholders.API.Public;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
            : base(mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Check if the user exists by username
        public Result<bool> Exists(string username)
        {
            // Return if the user exists
            return Result.Ok(_userRepository.Exists(username));
        }

        // Get active user by username
        public Result<UserDto> GetActiveByName(string username)
        {
            // Retrieve user from the repository
            var user = _userRepository.GetActiveByName(username);

            if (user == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError($"User with username '{username}' not found.");
            }

            // Map to DTO and return
            return Result.Ok(MapToDto(user));
        }

        // Create a new user
        public Result<UserDto> Create(UserDto userDto)
        {
            // Map DTO to domain object
            var user = MapToDomain(userDto);

            try
            {
                // Create user in repository
                var createdUser = _userRepository.Create(user);

                // Map created user back to DTO and return
                return Result.Ok(MapToDto(createdUser));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }

        // Get person ID by user ID
        public Result<long> GetPersonId(long userId)
        {
            try
            {
                // Retrieve person ID from repository
                var personId = _userRepository.GetPersonId(userId);

                return Result.Ok(personId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        // Get person details by user ID
        public Result<PersonDto> GetPersonByUserId(long userId)
        {
            try
            {
                // Retrieve person from repository
                var person = _userRepository.GetPersonByUserId(userId);

                if (person == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError($"Person with user ID '{userId}' not found.");
                }

                // Map the person to DTO and return
                var personDto = _mapper.Map<PersonDto>(person); 
                return Result.Ok(personDto);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}

