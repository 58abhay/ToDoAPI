using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoAPI.Application.CQRS.TaskModule.Commands;
using ToDoAPI.Application.CQRS.TaskModule.Handlers;
using ToDoAPI.Application.Interfaces;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Domain.Exceptions;

namespace ToDoAPI.UnitTests.CQRS
{
    public class CreateTaskItemHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepoMock;
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly CreateTaskItemHandler _handler;
        private readonly Guid _testAccountId = Guid.NewGuid();

        public CreateTaskItemHandlerTests()
        {
            _taskRepoMock = new Mock<ITaskRepository>();
            _accountRepoMock = new Mock<IAccountRepository>();
            _handler = new CreateTaskItemHandler(_taskRepoMock.Object, _accountRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedTask()
        {
            var command = new CreateTaskItemCommand("Test task", false, _testAccountId);
            var createdTask = new TaskItem { Description = "Test task", IsCompleted = false };

            _taskRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal("Test task", result.Description);
            Assert.False(result.IsCompleted);
        }

        [Fact]
        public async Task Handle_InvalidDescription_ThrowsValidationException()
        {
            var command = new CreateTaskItemCommand("", false, _testAccountId);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );
        }
    }
}