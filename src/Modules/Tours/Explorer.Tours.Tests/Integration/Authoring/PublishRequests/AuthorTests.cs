using Explorer.API.Controllers.Administrator.Execution;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.PublishRequests;
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
    public class AuthorTests : BaseToursIntegrationTest
    {
        public AuthorTests(ToursTestFactory factory) : base(factory) { }

        
        [Theory]
        [InlineData(-1, -2, -3, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestStatus.Pending, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestType.KeyPoint, "")]
        [InlineData(-1, -3, -2, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestStatus.Pending, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestType.Object, "")]
        public void Creation_succeeds(int authorId, int adminId, int entityId, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestStatus status, Explorer.Tours.API.Dtos.PublishRequestDtos.PublishRequestDto.RegistrationRequestType type, string comment)
        {
           

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var publishRequestDto = new PublishRequestDto(comment, authorId, adminId, entityId, status, type);

            var result = (ObjectResult)controller.Create(publishRequestDto).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedEntity = dbContext.PublishRequests.FirstOrDefault(p => p.EntityId == entityId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(Core.Domain.PublishRequests.RegistrationRequestStatus.Pending);
            
        }

        private static PublishRequestController CreateController(IServiceScope scope)
        {
            return new PublishRequestController(scope.ServiceProvider.GetRequiredService<IPublishRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
