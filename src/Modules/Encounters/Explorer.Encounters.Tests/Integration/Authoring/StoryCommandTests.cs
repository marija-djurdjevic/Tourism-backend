using Explorer.API.Controllers.Author.Authoring;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Tests.Integration.Authoring
{
    [Collection("Sequential")]
    public class StoryCommandTests : BaseEncountersIntegrationTest
    {
        public StoryCommandTests(EncountersTestFactory factory) : base(factory) { }


        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, "-11");
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var newEntity = new StoryDto
            {
                Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.",
                AuthorId = -11,
                BookId = -1,
                Title = "Loren ipsum perspiciatis",
                ImageId = 0,
                StoryStatus = StoryStatus.Pending
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity, 2).Result)?.Value as StoryDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntity = dbContext.Stories.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);

        }


        private static StoryController CreateController(IServiceScope scope, string userId)
        {
            return new StoryController(scope.ServiceProvider.GetRequiredService<IStoryService>(), scope.ServiceProvider.GetRequiredService<IPublishRequestService>(), scope.ServiceProvider.GetRequiredService<IKeyPointService>())
            {
                ControllerContext = BuildContext(userId)
            };
        }
    }
}
