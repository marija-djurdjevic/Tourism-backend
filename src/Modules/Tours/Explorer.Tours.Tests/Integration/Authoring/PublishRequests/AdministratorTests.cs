using Explorer.API.Controllers.Author.Execution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Authoring.PublishRequests
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

            var result = ((ObjectResult)controller.GetAll().Result)?.Value as PagedResult<PublishRequestDto>;

            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }

        private static PublishRequestController CreateController(IServiceScope scope)
        {
            return new PublishRequestController(scope.ServiceProvider.GetRequiredService<IPublishRequestService>(), scope.ServiceProvider.GetRequiredService<IKeyPointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
