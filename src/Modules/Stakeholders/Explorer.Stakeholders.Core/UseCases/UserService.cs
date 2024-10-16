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

        
        public Result<bool> Exists(string username)
        {
            
            return Result.Ok(_userRepository.Exists(username));
        }

        public Result<UserDto> GetActiveByName(string username)
        {
          
            var user = _userRepository.GetActiveByName(username);

            if (user == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError($"User with username '{username}' not found.");
            }

         
            return Result.Ok(MapToDto(user));
        }

    
        public Result<UserDto> Create(UserDto userDto)
        {
            
            var user = MapToDomain(userDto);

            try
            {

                var createdUser = _userRepository.Create(user);
                
                return Result.Ok(MapToDto(createdUser));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }

       
        public Result<long> GetPersonId(long userId)
        {
            try
            {
                
                var personId = _userRepository.GetPersonId(userId);

                return Result.Ok(personId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        
        public Result<PersonDto> GetPersonByUserId(long userId)
        {
            try
            {
                var person = _userRepository.GetPersonByUserId(userId);

                if (person == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError($"Person with user ID '{userId}' not found.");
                }

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

