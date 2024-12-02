using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Infrastructure.Database;
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

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class SalesTests : BasePaymentsIntegrationTest
    {
        public SalesTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Create()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new SaleDto
            {
                Discount = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                TourIds = new List<int> { 1, 2 },
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public void Delete()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();


            // Act
            var result = (OkResult)controller.Delete(-1);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedCourse = dbContext.Sales.FirstOrDefault(i => i.Id == -1);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Update()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var updatedEntity = new SaleDto
            {
                Id = -2,
                Discount = 33,
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as SaleDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Discount.ShouldBe(updatedEntity.Discount);

            var oldEntity = dbContext.Sales.FirstOrDefault(i => i.Discount == 25);
            oldEntity.ShouldBeNull();
        }
        private static SaleController CreateController(IServiceScope scope)
        {
            return new SaleController(scope.ServiceProvider.GetRequiredService<ISaleService>());

        }
    }
}
