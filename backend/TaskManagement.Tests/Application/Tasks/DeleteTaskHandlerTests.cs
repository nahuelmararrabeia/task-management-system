using FluentAssertions;
using Moq;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Tasks.Commands.DeleteTask;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Tests.Unit.TestData.Builders;

namespace TaskManagement.Tests.Unit.Application.Tasks
{
    public class DeleteTaskHandlerTests
    {
        [Fact]
        public async Task Should_Delete_Task()
        {
            var task = new TaskItemBuilder().Build();

            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            var handler = new DeleteTaskHandler(repoMock.Object);
            var command = new DeleteTaskCommand(task.Id);


            await handler.Handle(command, CancellationToken.None);

            task.IsDeleted.Should().BeTrue();
            task.DeletedAt.Should().NotBeNull();
            repoMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_NotFound_When_Task_Does_Not_Exist()
        {
            // GIVEN
            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskItem?)null);

            var handler = new DeleteTaskHandler(repoMock.Object);

            var command = new DeleteTaskCommand(Guid.NewGuid());

            // WHEN / THEN
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
            repoMock.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Never);
        }
    }
}
