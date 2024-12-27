using Explorer.API.Controllers.Administrator;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class BookCommandTest : BaseEncountersIntegrationTest
    {
        public BookCommandTest(EncountersTestFactory factory) : base(factory) { }


        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, "-1");
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var newEntity = new BookDto
            {
                Title = "Grcke mitologije",
                AdminId = -1,
                PageNum = 0,
                BookColour = "#eea0c6"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BookDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntity = dbContext.Books.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);

        }


        private static BookController CreateController(IServiceScope scope, string userId)
        {
            return new BookController(scope.ServiceProvider.GetRequiredService<IBookService>())
            {
                ControllerContext = BuildContext(userId)
            };
        }
    }
}
