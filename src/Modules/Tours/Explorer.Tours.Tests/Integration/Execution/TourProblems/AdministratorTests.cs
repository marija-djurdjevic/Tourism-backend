using Explorer.API.Controllers.Administrator;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            private static TourProblemController CreateController(IServiceScope scope)
            {
                return new TourProblemController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
                {
                    ControllerContext = BuildContext("-1")
                };
            }
    }
}
