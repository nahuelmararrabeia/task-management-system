using FluentAssertions;
using Moq;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Users.Commands;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Tests.Unit.Application.Users
{
    public class CreateUserHandlerTests
    {
        [Fact]
        public async Task Should_Create_New_User()
        {
            var repoMock = new Mock<IUserRepository>();

            repoMock
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateUserHandler(repoMock.Object);

            var command = new CreateUserCommand("test@test.com", "username", "admin123!");


            var result = await handler.Handle(command, CancellationToken.None);


            result.Should().NotBe(Guid.Empty);
            repoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Should_Fail_When_Repository_Throws()
        {
            var repoMock = new Mock<IUserRepository>();

            repoMock
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("DB error"));

            var handler = new CreateUserHandler(repoMock.Object);

            var command = new CreateUserCommand("test@test.com", "username", "admin123!");

            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
