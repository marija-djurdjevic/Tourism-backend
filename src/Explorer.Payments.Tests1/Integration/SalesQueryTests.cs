using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
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
    public class SalesQueryTests : BasePaymentsIntegrationTest
    {
        public SalesQueryTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<SaleDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }
        private static SaleController CreateController(IServiceScope scope)
        {
            return new SaleController(scope.ServiceProvider.GetRequiredService<ISaleService>());

        }
    }
}
