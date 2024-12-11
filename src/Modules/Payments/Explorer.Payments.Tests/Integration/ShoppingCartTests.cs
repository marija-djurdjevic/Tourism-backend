using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Tests;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Internal.Shopping;
using Explorer.Payments.Core.Domain.Shopping;


namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class ShoppingCartTests : BasePaymentsIntegrationTest
    {
        public ShoppingCartTests(PaymentsTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData("-1", true, 200.00, true)]
        [InlineData("-1", false, 0.00, false)]
        public void Purchases_Tour_Test(string userId, bool shouldSucceed, double expectedTotalPrice, bool shouldStoreInDatabase)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            dbContext.ShouldNotBeNull();

            // Set up user claims
            var claims = new List<Claim> { new Claim("id", userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Define order items
            List<OrderItemDto> orderItems = new List<OrderItemDto>
            {
                new OrderItemDto(2, "Desert Adventure", 120.00),
                new OrderItemDto(3, "City Lights", 80.00)
            };

            // Act
            var result = ((ObjectResult)controller.Checkout(orderItems).Result)?.Value as ShoppingCartDto;

            // Assert - Result and Total Price
            if (shouldSucceed)
            {
                result.ShouldNotBeNull();
                result.TotalPrice.ShouldBe(expectedTotalPrice);
            }
            else
            {
                result.ShouldBeNull();
            }

            // Assert - Database Check
            var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.TouristId == int.Parse(userId));
            var tokensEntities = dbContext.TourPurchaseTokens
             .Where(i => i.TouristId == -1 && (i.TourId == 2 || i.TourId == 3))
             .ToList();
            tokensEntities.Count.ShouldBe(2);
            storedEntity.ShouldNotBeNull();
        }


        [Fact]
        public void Successfully_gets_purchased_tours()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            controller.ShouldNotBeNull();

            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var claims = new List<Claim>
            {
                new Claim("id", "-2")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            List<OrderItemDto> orderItems = new List<OrderItemDto>
            {
                new OrderItemDto(-1, "Desert Adventure", 120.00),
                new OrderItemDto(-2, "City Lights", 80.00)
            };

            // Act
            var result = ((ObjectResult)controller.Checkout(orderItems).Result)?.Value as ShoppingCartDto;
            var result2 = ((ObjectResult)controller.GetPurchasedTours().Result)?.Value as List<TourDto>;
            result2.Count.ShouldBe(2);
        }

        [Fact]
        public void Successfully_refunds_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            controller.ShouldNotBeNull();

            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var claims = new List<Claim>
            {
                new Claim("id", "-2")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Act
            var result = ((ObjectResult)controller.Refund(-2).Result)?.Value as TourDto;

            var storedEntity = dbContext.TourPurchaseTokens.FirstOrDefault(i => i.TourId == result.Id && i.TouristId == -2);
            storedEntity.Refunded.ShouldBe(true);

        }

        [Theory]
        [InlineData(-2, -22, BundleStatus.Published)]
        public void PusrchaseBundle(int bundleId, int touristId, BundleStatus expectedStatus)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var bundle = dbContext.Bundles.FirstOrDefault(b => b.Id == bundleId);
            bundle.ShouldNotBeNull();
            bundle.Status.ShouldBe(expectedStatus);
            var newBundle = new BundleDto
            {
                Id = bundleId,
                AuthorId = bundle.AuthorId,
                TourIds = bundle.TourIds.ToList(),
                Price = bundle.Price,
                Status = BundleDto.BundleStatus.Published,
            };

            // Act
            var result = ((ObjectResult)controller.Purchase(newBundle, touristId).Result)?.Value as PaymentRecordDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(touristId);
            result.BundleId.ShouldBe((int)newBundle.Id);
            result.Price.ShouldBe(newBundle.Price);

            // Assert - Database
            var storedEntity = dbContext.PaymentRecords.FirstOrDefault(pr => pr.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.TouristId.ShouldBe(touristId);
            storedEntity.BundleId.ShouldBeEquivalentTo(newBundle.Id);
            storedEntity.Price.ShouldBe(newBundle.Price);

            // varies based on other tests
            var tokensEntities = dbContext.TourPurchaseTokens
             .Where(i => i.TouristId == touristId && (i.TourId == -2 || i.TourId == -3))
             .ToList();
            tokensEntities.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        private static ShoppingController CreateController(IServiceScope scope)
        {
            return new ShoppingController(scope.ServiceProvider.GetRequiredService<IShoppingService>(), scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<ITourSessionService>(), scope.ServiceProvider.GetRequiredService<ITourReviewService>(), scope.ServiceProvider.GetRequiredService<ITourPurchaseTokenService>(), scope.ServiceProvider.GetRequiredService<INotificationService>(), scope.ServiceProvider.GetRequiredService<IPaymentRecordService>(), scope.ServiceProvider.GetRequiredService<IBundleService>());

        }
    }
}
