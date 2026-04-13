using FluentAssertions;
using Moq;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Tests.Application.Tasks
{
    public class CreateTaskHandlerTests
    {
        [Fact]
        public async Task Should_Create_New_Task()
        {
            var repoMock = new Mock<ITaskRepository>();

            repoMock
                .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateTaskHandler(repoMock.Object);

            var command = new CreateTaskCommand("Test Task", "Description");

            
            var result = await handler.Handle(command, CancellationToken.None);

            
            result.Should().NotBe(Guid.Empty);
            repoMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task Should_Fail_When_Repository_Throws()
        {
            var repoMock = new Mock<ITaskRepository>();

            repoMock
                .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .ThrowsAsync(new Exception("DB error"));

            var handler = new CreateTaskHandler(repoMock.Object);

            var command = new CreateTaskCommand("Test", "Desc");

            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
