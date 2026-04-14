using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Renci.SshNet.Sftp;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.AssignUser;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Users.Commands.CreateUser;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks
{
    [Collection("IntegrationTests")]
    public class AssignUserToTaskTests
    {
        private readonly PostgresFixture _fixture;

        public AssignUserToTaskTests(PostgresFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Assign_User_To_Task()
        {
            // GIVEN
            var createTaskRequest = new CreateTaskRequestBuilder().Build();
            var createUserRequest = new CreateUserRequestBuilder().Build();

            var createTaskResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createTaskRequest);
            var createUserResponse = await _fixture.Client.PostAsJsonAsync("/api/user", createUserRequest);
            var task = await createTaskResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();
            var user = await createUserResponse.Content.ReadFromJsonAsync<CreateUserResponse>();

            // WHEN
            var assignRequest = new AssignUserToTaskCommand(task!.Id, user!.Id);

            var assignResponse = await _fixture.Client.PutAsync($"/api/tasks/{task.Id}/assign/{user.Id}", null);

            // THEN
            assignResponse.EnsureSuccessStatusCode();

            using var scope = _fixture.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var updatedTask = await db.Tasks.FindAsync(task.Id);

            updatedTask!.AssignedUserId.Should().Be(user.Id);
        }
    }
}
