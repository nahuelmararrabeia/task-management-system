using FluentAssertions;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Queries.GetTasks;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks
{
    [Collection("IntegrationTests")]
    public class GetTasksIntegrationTests
    {
        private readonly PostgresFixture _fixture;

        public GetTasksIntegrationTests(PostgresFixture fixture) {
            _fixture = fixture;

            _fixture.ResetDatabaseAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task Should_Return_Tasks_List()
        {
            // GIVEN
            var existingTasks = new List<CreateTaskCommand> {
                new CreateTaskRequestBuilder().WithTitle("Task 1").Build(),
                new CreateTaskRequestBuilder().WithTitle("Task 2").Build()
            };

            await CreateTasks(existingTasks);

            // WHEN
            var response = await _fixture.Client.GetAsync("/api/tasks");

            // THEN
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GetTasksResponse>();

            result.Should().NotBeNull();
            result!.Items.Should().HaveCount(existingTasks.Count);
        }

        [Fact]
        public async Task Should_Return_Empty_List_When_No_Tasks()
        {
            var response = await _fixture.Client.GetAsync("/api/tasks");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GetTasksResponse>();

            result!.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task Should_Not_Return_Deleted_Task_In_GetAll()
        {
            // GIVEN
            var createRequest = new CreateTaskRequestBuilder().Build();

            var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createRequest);
            var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

            await _fixture.Client.DeleteAsync($"/api/tasks/{created!.Id}");

            // WHEN
            var response = await _fixture.Client.GetAsync("/api/tasks");

            // THEN
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GetTasksResponse>();

            result!.Items.Should().NotContain(t => t.Id == created.Id);
        }

        private async Task CreateTasks(List<CreateTaskCommand> tasks)
        {
            foreach (var task in tasks) {
                await _fixture.Client.PostAsJsonAsync("/api/tasks", task);
            }
        }
    }
}
