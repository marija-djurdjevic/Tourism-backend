using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class BundleTests : BasePaymentsIntegrationTest
    {
        public BundleTests(PaymentsTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(1, new[] { -1, -2 }, 199.99, BundleStatus.Draft, "TitleTest")]
        public void CreateBundle(int authorId, int[] tourIds, double price, BundleStatus expectedStatus, string title)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var newBundle = new BundleDto
            {
                AuthorId = authorId,
                TourIds = tourIds.ToList(),
                Price = price,
                Status = BundleDto.BundleStatus.Draft,
                Title = title,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newBundle).Result)?.Value as BundleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.AuthorId.ShouldBe(newBundle.AuthorId);
            result.TourIds.ShouldBeEquivalentTo(newBundle.TourIds);
            result.Price.ShouldBe(newBundle.Price);
            result.Status.ShouldBe(BundleDto.BundleStatus.Draft);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(b => b.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.AuthorId.ShouldBe(newBundle.AuthorId);
            storedEntity.TourIds.ShouldBeEquivalentTo(newBundle.TourIds);
            storedEntity.Price.ShouldBe(newBundle.Price);
            storedEntity.Status.ShouldBe(BundleStatus.Draft);
        }

        [Theory]
        [InlineData(-1, new[] { -1, -2 }, 199.99, BundleStatus.Published, "TitleUpdated")]
        public void UpdateBundle(int authorId, int[] tourIds, double price, BundleStatus newStatus, string title)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var updatedBundle = new BundleDto
            {
                Id = -1,
                AuthorId = authorId,
                TourIds = tourIds.ToList(),
                Price = price,
                Status = BundleDto.BundleStatus.Published,
                Title = title,
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedBundle).Result)?.Value as BundleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(updatedBundle.Id);
            result.AuthorId.ShouldBe(updatedBundle.AuthorId);
            result.TourIds.ShouldBeEquivalentTo(updatedBundle.TourIds);
            result.Price.ShouldBe(updatedBundle.Price);
            result.Status.ShouldBe(BundleDto.BundleStatus.Published);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(b => b.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.AuthorId.ShouldBe(updatedBundle.AuthorId);
            storedEntity.TourIds.ShouldBeEquivalentTo(updatedBundle.TourIds);
            storedEntity.Price.ShouldBe(updatedBundle.Price);
            storedEntity.Status.ShouldBe(BundleStatus.Published);
        }

        private static BundleController CreateController(IServiceScope scope)
        {
            return new BundleController(scope.ServiceProvider.GetRequiredService<IBundleService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
