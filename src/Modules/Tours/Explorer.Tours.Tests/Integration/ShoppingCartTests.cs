using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Infrastructure.Database;
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
using Explorer.Tours.API.Public.Execution;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class ShoppingCartTests : BaseToursIntegrationTest
    {
        public ShoppingCartTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData("-1", true, 200.00, true)]
        [InlineData("-1", false, 0.00, false)]
        public void Purchases_Tour_Test(string userId, bool shouldSucceed, double expectedTotalPrice, bool shouldStoreInDatabase)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
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

            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var claims = new List<Claim>
                {
                new Claim("id", "-1")
                };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            List<OrderItemDto> orderItems = new List<OrderItemDto>
                {
                    new OrderItemDto(2, "Desert Adventure", 120.00),
                    new OrderItemDto(3, "City Lights", 80.00)
                };

            // Act
            var result = ((ObjectResult)controller.Checkout(orderItems).Result)?.Value as ShoppingCartDto;
            var result2 = ((ObjectResult)controller.GetPurchasedTours().Result)?.Value as List<TourDto>;
            result2.Count.ShouldBe(2);
        }

        private static ShoppingController CreateController(IServiceScope scope)
        {
            return new ShoppingController(scope.ServiceProvider.GetRequiredService<IShoppingService>(), scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<ITourSessionService>(), scope.ServiceProvider.GetRequiredService<ITourReviewService>());

        }
    }
}
