using Explorer.API.Controllers.Administrator;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;
using ProblemDetails = Explorer.Tours.Core.Domain.TourProblems.ProblemDetails;
using ProblemStatus = Explorer.Tours.API.Dtos.TourProblemDtos.ProblemStatus;

namespace Explorer.Tours.Tests.Integration.TourProblems
{
    [Collection("Sequential")]
    public class TourProblemControllerTests : BaseToursIntegrationTest
    {
        public TourProblemControllerTests(ToursTestFactory factory) : base(factory) { }

       
        [Theory]
        [InlineData(1, 1 )]
        [InlineData(1, 2)]
        public void GetAll_ReturnsPagedResultOfTourProblemDtos(int tourId, int touristId)
        {
            // Arrange
            var details = new ProblemDetails(Core.Domain.TourProblems.ProblemCategory.RoadObstacles, 1, "drvo na putu", new DateTime(2024, 10, 29, 10, 53, 25));
            var detailsDto = new ProblemDetailsDto
            {
                Category = (API.Dtos.TourProblemDtos.ProblemCategory)API.Dtos.TourProblemDtos.ProblemCategory.RoadObstacles,
                Time = new DateTime(2024, 10, 29, 10, 53, 25),
                ProblemPriority = 1,
                Explanation = "drvo na putu"
            };

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            
            var tourProblemDto = new TourProblemDto
            {
                TourId = tourId,
                //TouristId = 1,
                Details = detailsDto,
                Status = ProblemStatus.Pending 
            };


            // Act
            var result = controller.GetAll().Result as ObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2 )]
        public void SetDeadline_SetsDeadlineAndReturnsUpdatedTourProblem(int tourId, int touristId)
        {
            // Arrange
            var details = new ProblemDetails(Core.Domain.TourProblems.ProblemCategory.RoadObstacles, 1, "drvo na putu", new DateTime(2024, 10, 29, 10, 53, 25));
            var detailsDto = new ProblemDetailsDto
            {
                Category = (API.Dtos.TourProblemDtos.ProblemCategory)API.Dtos.TourProblemDtos.ProblemCategory.RoadObstacles,
                Time = new DateTime(2024, 10, 29, 10, 53, 25),
                ProblemPriority = 1,
                Explanation = "drvo na putu"
            };

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourProblemDto = new TourProblemDto
            {
                TourId = tourId, 
                Details = detailsDto,
                Status = ProblemStatus.Pending
            };
           
            var deadline = DateTime.Now.AddDays(1);
            var result = (ObjectResult)controller.SetDeadline((int)tourProblemDto.Id, deadline).Result;


            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            var storedEntity = dbContext.TourProblems.FirstOrDefault(t => t.Id == tourId);
            storedEntity.Deadline.ShouldBe(deadline);

        }

        private static TourProblemController CreateController(IServiceScope scope)
        {
            return new TourProblemController(
                scope.ServiceProvider.GetRequiredService<ITourProblemService>(),
                scope.ServiceProvider.GetRequiredService<ITourService>())
                   {
                ControllerContext = BuildContext("-1")
                   };
        }
    }
}
