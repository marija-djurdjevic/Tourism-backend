﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.Tours.API.Public.Administration;
using Explorer.API.Controllers.Author;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class KeyPointQueryTests : BaseToursIntegrationTest
    {
        public KeyPointQueryTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void Retrives_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            //Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<KeyPointDto>;
            //Assert 
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }
        public static KeyPointController CreateController(IServiceScope scope)
        {
            return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
