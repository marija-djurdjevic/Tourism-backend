using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserRatingService : BaseService<UserRatingDto, UserRating>, IUserRatingService
    {
        private readonly IUserRatingRepository _userRatingRepository;

        public UserRatingService(IUserRatingRepository userRatingRepository, IMapper mapper)
            : base(mapper)
        {
            _userRatingRepository = userRatingRepository;
        }

        public Result<List<UserRatingDto>> GetAll()
        {
            // Retrieve all ratings from the repository
            var result = _userRatingRepository.GetAll(); 

            // Map to DTO and return
            return MapToDto(result);
        }

        public Result<string> SubmitRating(UserRatingDto ratingDto)
        {
            // Map DTO to domain object
            var userRating = MapToDomain(ratingDto);

            try
            {
                // Create the new user rating in the repository
                var createdRating = _userRatingRepository.Create(userRating);

                // Map back to DTO and return
                return Result.Ok<string>("Rating submited successfully!");
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

  
    }
}
