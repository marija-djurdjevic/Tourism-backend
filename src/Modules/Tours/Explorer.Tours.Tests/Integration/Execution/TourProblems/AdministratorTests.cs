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
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.API.Controllers.Administrator.Execution;
using Explorer.Tours.API.Public.Authoring;

namespace Explorer.Tours.Tests.Integration.Execution.TourProblems
{
    [Collection("Sequential")]
    public class AdministratorTests : BaseToursIntegrationTest
    {
        public AdministratorTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void GetAll_Success()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var details = new ProblemDetails(Core.Domain.TourProblems.ProblemCategory.RoadObstacles, 1, "drvo na putu", new DateTime(2024, 10, 29, 10, 53, 25));

            var result = ((ObjectResult)controller.GetAll().Result)?.Value as PagedResult<TourProblemDto>;

            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void SetDeadline_Success(int tourProblemId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var deadline = DateTime.SpecifyKind(new DateTime(2024, 11, 3, 10, 53, 25), DateTimeKind.Utc);

            var result = (ObjectResult)controller.SetDeadline(tourProblemId, deadline).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            var storedEntity = dbContext.TourProblems.FirstOrDefault(t => t.Id == tourProblemId);
            storedEntity.Deadline.ShouldBe(deadline);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void Update_Success(int tourProblemId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            controller.SetDeadline(tourProblemId, DateTime.SpecifyKind(new DateTime(2024, 11, 3, 10, 53, 25), DateTimeKind.Utc));
            var problem = dbContext.TourProblems.FirstOrDefault(t => t.Id == tourProblemId);
            TourProblemDto problemDto = convertToDto(problem);

            var updatedResult = (ObjectResult)controller.Update(problemDto).Result;

            updatedResult.ShouldNotBeNull();
            updatedResult.StatusCode.ShouldBe(200);
            var finalUpdatedProblem = updatedResult?.Value as TourProblemDto;
            finalUpdatedProblem.Status.ShouldBe(ProblemStatus.Expired);
        }

        private static TourProblemController CreateController(IServiceScope scope)
        {
            return new TourProblemController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                    ControllerContext = BuildContext("-1")
            };
        }

        private TourProblemDto convertToDto(TourProblem problem)
        {
            return new TourProblemDto((int)problem.Id, problem.TourId, new ProblemDetailsDto((API.Dtos.TourProblemDtos.ProblemCategory)problem.Details.Category, problem.Details.ProblemPriority, problem.Details.Explanation, problem.Details.Time), null, ProblemStatus.Expired, problem.Deadline);
        }
    }
}
