using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemCategory = Explorer.Tours.Core.Domain.TourProblems.ProblemCategory;
using ProblemDetails = Explorer.Tours.Core.Domain.TourProblems.ProblemDetails;
using ProblemStatus = Explorer.Tours.API.Dtos.TourProblemDtos.ProblemStatus;

namespace Explorer.Tours.Tests.Integration.Execution.TourProblems
{
    [Collection("Sequential")]
    public class TouristTests : BaseToursIntegrationTest
    {
        public TouristTests(ToursTestFactory factory) : base(factory) {}

        [Theory]
        [InlineData(-1, 1, ProblemStatus.Pending)]
        [InlineData(-1, 2, ProblemStatus.Pending)]
        public void Creation_succeeds(int tourId, int touristId, ProblemStatus status)
        {
            ProblemDetailsDto detailsDto = new ProblemDetailsDto((API.Dtos.TourProblemDtos.ProblemCategory)ProblemCategory.UnclearInstructions, 0, "Nejasno", new DateTime(2024, 10, 29, 10, 53, 25));

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourProblemDto = new TourProblemDto(tourId, touristId, detailsDto, null, status, null);

            var result = (ObjectResult)controller.Create(tourProblemDto).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedEntity = dbContext.TourProblems.FirstOrDefault(t => t.TourId == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(Core.Domain.TourProblems.ProblemStatus.Pending);
        }

        private static TourProblemController CreateController(IServiceScope scope)
        {
            return new TourProblemController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
