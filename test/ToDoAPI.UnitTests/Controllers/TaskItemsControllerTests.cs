using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoAPI.API.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Wrappers;

public class TaskItemsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<ILogger<TaskItemsController>> _loggerMock = new();

    [Fact]
    public async Task GetById_ReturnsOkResult_WithTaskItemWrappedInApiResponse()
    {
        // Arrange
        var taskId = Guid.NewGuid(); //  Changed from int to Guid
        var taskEntity = new TaskItem
        {
            Id = taskId,
            Description = "Mocked Controller Task",
            IsCompleted = false
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetTaskItemByIdQuery>(q => q.Id == taskId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskEntity);

        var controller = new TaskItemsController(_mediatorMock.Object, _loggerMock.Object);

        // Inject a mock HttpContext to avoid NullReferenceException
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.GetById(taskId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsAssignableFrom<ApiResponse<TaskItem?>>(okResult.Value);
        Assert.Equal(taskEntity.Description, response.Data?.Description);
    }
}