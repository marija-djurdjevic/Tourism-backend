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
            var result = _userRatingRepository.GetAll(); 

            return MapToDto(result);
        }

        public Result<UserRatingDto> Create(UserRatingDto ratingDto, string userId, string username)
        {
            var userRating = MapToDomain(ratingDto);


            try
            {
                var createdRating = _userRatingRepository.Create(new UserRating(userRating.Rating, userRating.Comment, int.Parse(userId), username));
                return Result.Ok(MapToDto(createdRating));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
