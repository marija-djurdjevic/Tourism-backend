using Explorer.API.Controllers.Author.Execution;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AuthorTests : BaseToursIntegrationTest
    {
        public AuthorTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData("Vranje", ProblemCommentType.FromAuthor, -1)]
        public void AddComment_succeeds(string content, ProblemCommentType type, int senderId)
        {
            DateTime sentTime = new DateTime(2024, 10, 29, 10, 53, 25);
            ProblemCommentDto commentDto = new ProblemCommentDto
            {
                Content = content,
                Type = type,
                SenderId = senderId,
                SentTime = sentTime
            };

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = (ObjectResult)controller.AddComment(-1, commentDto).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedEntity = dbContext.TourProblems.FirstOrDefault(t => t.Id == -1);
            storedEntity.Comments.ShouldHaveSingleItem();
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
