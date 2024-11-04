using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using ProblemDetails = Explorer.Tours.Core.Domain.TourProblems.ProblemDetails;
using ProblemStatus = Explorer.Tours.API.Dtos.TourProblemDtos.ProblemStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProblemCategory = Explorer.Tours.Core.Domain.TourProblems.ProblemCategory;
using Explorer.API.Controllers.Administrator.Execution;

namespace Explorer.Tours.Tests.Integration.Execution.TourProblems
{
    [Collection("Sequential")]
    public class AdministratorTests : BaseToursIntegrationTest
    {
        public AdministratorTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(1, ProblemStatus.Pending)]
        [InlineData(2, ProblemStatus.Pending)]
        public void Update_succeeds(int tourProblemId, ProblemStatus newStatus)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // First, create a TourProblem to update
      
            var initialDto = new TourProblemDto
            {
                TourId = 1,
               // TouristId = 1,
                Details = new ProblemDetailsDto
                {
                    Category = (API.Dtos.TourProblemDtos.ProblemCategory)ProblemCategory.UnclearInstructions,
                    Time = DateTime.UtcNow,
                    ProblemPriority = 0,
                    Explanation = "Nejasno"
                },
                Status = ProblemStatus.Closed
            };

                var createResult = (ObjectResult)controller.Update(initialDto).Result;
                createResult.StatusCode.ShouldBe(200);


                var updateResult = (ObjectResult)controller.Update(initialDto).Result; // Assuming you have an Update method in your controller

                // Assert
                updateResult.ShouldNotBeNull();
                updateResult.StatusCode.ShouldBe(200);

                // Verify the update in the database
                var updatedEntity = dbContext.TourProblems.FirstOrDefault(tp => tp.Id == initialDto.Id);
                updatedEntity.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(1, 1)]
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
        [InlineData(1, 2)]
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
            return new TourProblemController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                    ControllerContext = BuildContext("-1")
            };
        }
    }
}
