using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.ChangeStatus;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Domain.Enums;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks
{
    [Collection("IntegrationTests")]
    public class ChangeTaskStatusIntegrationTests
    {
        private readonly PostgresFixture _fixture;
        public ChangeTaskStatusIntegrationTests(PostgresFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Change_Task_Status()
        {
            // GIVEN
            var taskReq = new CreateTaskRequestBuilder().Build();
            var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", taskReq);
            var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

            // WHEN
            var changeStatusReq = new ChangeTaskStatusCommand(created!.Id, TaskItemStatus.Completed);
            var response = await _fixture.Client.PatchAsJsonAsync(
                $"/api/tasks/{created!.Id}/status",
                changeStatusReq
            );

            // THEN
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var updatedTaskResponse = await _fixture.Client.GetAsync($"/api/tasks/{created.Id}");
            var updatedTask = await updatedTaskResponse.Content.ReadFromJsonAsync<GetTaskByIdResponse>();

            updatedTask!.Status.Should().Be(TaskItemStatus.Completed);
        }
    }
}
