using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoAPI.Application.CQRS.TaskModule.Handlers;
using ToDoAPI.Application.CQRS.TaskModule.Queries;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.UnitTests.CQRS
{
    public class GetTaskItemByIdHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock = new();
        private readonly Mock<IAccountRepository> _accountRepoMock = new();
        private readonly GetTaskItemByIdHandler _handler;

        public GetTaskItemByIdHandlerTests()
        {
            _handler = new GetTaskItemByIdHandler(_taskRepoMock.Object, _accountRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsTaskItem()
        {
            var taskId = Guid.NewGuid();
            var task = new TaskItem { Id = taskId, Description = "Test", IsCompleted = false };

            _taskRepoMock.Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>())).ReturnsAsync(task);

            var result = await _handler.Handle(new GetTaskItemByIdQuery(taskId), CancellationToken.None);

            Assert.Equal("Test", result.Description);
            Assert.False(result.IsCompleted);
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var invalidId = Guid.NewGuid();

            _taskRepoMock.Setup(r => r.GetByIdAsync(invalidId, It.IsAny<CancellationToken>()))
                         .ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new GetTaskItemByIdQuery(invalidId), CancellationToken.None)
            );
        }
    }
}