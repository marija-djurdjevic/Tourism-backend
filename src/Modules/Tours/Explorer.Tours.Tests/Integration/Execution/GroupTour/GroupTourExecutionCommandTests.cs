using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Explorer.API.Controllers.Tourist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Shouldly;
using Explorer.Tours.API.Public.Authoring;
using Explorer.API.Controllers.Tourist.Execution;

namespace Explorer.Tours.Tests.Integration.Execution.GroupTour
{

    [Collection("Sequential")]
    public class GroupTourExecutionCommandTests : BaseToursIntegrationTest
    {
        public GroupTourExecutionCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new GroupTourExecutionDto
            {
                
                GroupTourId = 1,
                TouristId = 1,
                IsFinished = false
               
            };
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as GroupTourExecutionDto;
            //Assert response 
            result.ShouldNotBeNull();
            result.GroupTourId.ShouldNotBe(0);
            result.TouristId.ShouldBe(newEntity.TouristId);
            result.IsFinished.ShouldBe(newEntity.IsFinished);

            //Assert database
            var storedEntity = dbContext.GroupTourExecution.FirstOrDefault(i => i.GroupTourId == newEntity.GroupTourId);
            storedEntity.ShouldNotBeNull();
            //storedEntity.GroupTourId.ShouldBe(result.GroupTourId);
        }

        private static GroupTourExecutionController CreateController(IServiceScope scope)
        {
            return new GroupTourExecutionController(scope.ServiceProvider.GetRequiredService<IGroupTourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }

   
}
