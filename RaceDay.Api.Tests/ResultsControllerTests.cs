
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Api.Tests
{
    public class ResultsControllerTests
    {
        [Fact]
        public async Task GetResult_ExistingId_ReturnsResult()
        {
            // Arrange: create an isolated in-memory database.
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new RaceDayDbContext(options);

            context.Results.Add(new Result
            {
                ResultId = 1,
                EnrolmentId = 1
            });

            await context.SaveChangesAsync();

            var controller = new ResultsController(context);

            // Act: request result ID 1.
            var response = await controller.GetResult(1);

            // Assert: HTTP 200 with the requested result.
            var okResult = Assert.IsType<OkObjectResult>(
                response.Result);

            var result = Assert.IsType<Result>(okResult.Value);

            Assert.Equal(1, result.ResultId);
        }

        [Fact]
        public async Task GetResult_NonExistingId_ReturnsNotFound()
        {
            // Arrange: create an empty, isolated in-memory database.
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using var context = new RaceDayDbContext(options);

            var controller = new ResultsController(context);

            // Act: request a result ID that does not exist.
            var response = await controller.GetResult(999999);

            // Assert: HTTP 404 Not Found.
            Assert.IsType<NotFoundResult>(response.Result);
        }
    }
}
