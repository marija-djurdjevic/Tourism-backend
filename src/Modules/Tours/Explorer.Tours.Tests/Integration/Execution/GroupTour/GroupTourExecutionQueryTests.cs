using Explorer.API.Controllers.Author.Execution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Execution.GroupTour
{
    [Collection("Sequential")]
    public class GroupTourExecutionQueryTests : BaseToursIntegrationTest
    {
        public GroupTourExecutionQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrives_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            //Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<GroupTourExecutionDto>;
            //Assert 
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }
        public static GroupTourExecutionController CreateController(IServiceScope scope)
        {
            return new GroupTourExecutionController(scope.ServiceProvider.GetRequiredService<IGroupTourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
