using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourPreferencesCommandTests : BaseToursIntegrationTest
    {
        public TourPreferencesCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourPreferencesDto
            {
                Difficulty = (TourDto.DifficultyStatus)DifficultyStatus.Medium,
                TouristId = 1,
                WalkingRating = 3,
                CyclingRating = 2,
                DrivingRating = 1,
                SailingRating = 0,
                Tags = new List<string> { "tag1", "tag2" }
            };

            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourPreferencesDto;

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(newEntity.TouristId);

            var storedEntity = dbContext.TourPreferences.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_required_fields_zero()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourPreferencesDto { };

            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public void Updates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourPreferencesDto
            {
                Id = -1,
                Difficulty = (TourDto.DifficultyStatus)DifficultyStatus.Easy,
                WalkingRating = 3,
                CyclingRating = 2,
                DrivingRating = 1,
                SailingRating = 0,
                Tags = new List<string> { "tag11", "tag22" }
            };

            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourPreferencesDto;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Difficulty.ShouldBe(updatedEntity.Difficulty);
            result.WalkingRating.ShouldBe(updatedEntity.WalkingRating);
            result.CyclingRating.ShouldBe(updatedEntity.CyclingRating);
            result.DrivingRating.ShouldBe(updatedEntity.DrivingRating);
            result.SailingRating.ShouldBe(updatedEntity.SailingRating);

            var storedEntity = dbContext.TourPreferences.FirstOrDefault(i => i.Difficulty == DifficultyStatus.Easy);
            storedEntity.ShouldNotBeNull();
            storedEntity.Tags.ShouldBe(updatedEntity.Tags);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourPreferencesDto
            {
                Id = -1000,
            };

            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TourPreferencesController CreateController(IServiceScope scope)
        {
            return new TourPreferencesController(scope.ServiceProvider.GetRequiredService<ITourPreferencesService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
