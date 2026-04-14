using FluentAssertions;
using Moq;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Tasks.Commands.UpdateTask;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Tests.Application.Tasks
{
    public class UpdateTaskHandlerTests
    {
        [Fact]
        public async Task Should_Update_Task()
        {
            // GIVEN
            var task = new TaskItem("Old Title", "Old Desc");

            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            var handler = new UpdateTaskHandler(repoMock.Object);

            var command = new UpdateTaskCommand(
                task.Id,
                "New Title",
                "New Desc"
            );

            // WHEN
            await handler.Handle(command, CancellationToken.None);

            // THEN
            task.Title.Should().Be("New Title");
            task.Description.Should().Be("New Desc");

            repoMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_NotFound_When_Task_Does_Not_Exist()
        {
            // GIVEN
            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskItem?)null);

            var handler = new UpdateTaskHandler(repoMock.Object);

            var command = new UpdateTaskCommand(
                Guid.NewGuid(),
                "Title",
                "Desc"
            );

            // WHEN / THEN
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
            repoMock.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Never);
        }
    }
}
