using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks
{
    [Collection("IntegrationTests")]
    public class DeleteTaskIntegrationTests
    {
        private readonly PostgresFixture _fixture;
        public DeleteTaskIntegrationTests(PostgresFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Soft_Delete_Task()
        {
            // GIVEN
            var createRequest = new CreateTaskRequestBuilder().Build();

            var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createRequest);
            var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

            // WHEN
            var response = await _fixture.Client.DeleteAsync($"/api/tasks/{created!.Id}");

            // THEN
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            using var scope = _fixture.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var task = await db.Tasks
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == created.Id);

            task.Should().NotBeNull();
            task.IsDeleted.Should().BeTrue();
            task.DeletedAt.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Return_404_When_Deleting_Non_Existing_Task()
        {
            var response = await _fixture.Client.DeleteAsync($"/api/tasks/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
