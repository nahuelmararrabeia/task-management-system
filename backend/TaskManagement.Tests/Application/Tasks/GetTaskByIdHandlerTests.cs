using FluentAssertions;
using Moq;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Tests.Unit.TestData.Builders;

namespace TaskManagement.Tests.Application.Tasks
{
    public class GetTaskByIdHandlerTests
    {
        [Fact]
        public async Task Should_Get_Task_By_Id()
        {
            var repoMock = new Mock<ITaskRepository>();
            var taskItemMock = new TaskItemBuilder().Build();
            var guid = Guid.NewGuid();

            repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(taskItemMock);

            var handler = new GetTaskByIdHandler(repoMock.Object);

            var command = new GetTaskByIdQuery(guid);


            var result = await handler.Handle(command, CancellationToken.None);


            result.Title.Should().Be(taskItemMock.Title);
            result.Description.Should().Be(taskItemMock.Description);
            repoMock.Verify(r => r.GetByIdAsync(guid), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_NotFoundException_When_Task_Does_Not_Exist()
        {
            var repoMock = new Mock<ITaskRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var handler = new GetTaskByIdHandler(repoMock.Object);

            var command = new GetTaskByIdQuery(Guid.NewGuid());


            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_Fail_When_Repository_Throws()
        {
            var repoMock = new Mock<ITaskRepository>();

            repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("DB error"));

            var handler = new GetTaskByIdHandler(repoMock.Object);

            var command = new GetTaskByIdQuery(Guid.NewGuid());

            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
