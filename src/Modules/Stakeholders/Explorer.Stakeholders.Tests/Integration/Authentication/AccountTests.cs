using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database;

namespace Explorer.Stakeholders.Tests.Integration.Authentication;

[Collection("Sequential")]
public class AccountTests : BaseStakeholdersIntegrationTest
{
    public AccountTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<AccountReviewDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(7);
        result.TotalCount.ShouldBe(7);
    }

    private static AccountController CreateController(IServiceScope scope)
    {
        return new AccountController(scope.ServiceProvider.GetRequiredService<IAccountService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

    //[Fact]
    //public void Updates()
    //{
    //    // Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateController(scope);
    //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
    //    var updatedEntity = new AccountReviewDto
    //    {
    //        Id = -1,
    //        Username = "Marija",
    //        Role = "Administrator",
    //        IsActive = true
    //    };

    //    // Act
    //    var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as AccountReviewDto;

    //    // Assert - Response
    //    result.ShouldNotBeNull();
    //    result.Username.ShouldBe(updatedEntity.Username);
    //    result.Role.ShouldBe(updatedEntity.Role);
    //    result.IsActive.ShouldBe(updatedEntity.IsActive);

    //    // Assert - Database
    //    var storedEntity = dbContext.Users.FirstOrDefault(i => i.Username == "Marija");
    //    storedEntity.ShouldNotBeNull();
    //    //storedEntity.Role.ShouldBe(updatedEntity.Role);
    //    //var oldEntity = dbContext.Users.FirstOrDefault(i => i.Role == Administrator);
    //    //oldEntity.ShouldBeNull();
    //}

    //[Fact]
    //public void Update_fails_invalid_id()
    //{
    //    // Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateController(scope);
    //    var updatedEntity = new AccountReviewDto
    //    {
    //        Id = -1000,
    //    };

    //    // Act
    //    var result = (ObjectResult)controller.Update(updatedEntity).Result;

    //    // Assert
    //    result.ShouldNotBeNull();
    //    result.StatusCode.ShouldBe(404);
    //}
}
