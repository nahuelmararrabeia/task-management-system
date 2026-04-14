using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks;

[Collection("IntegrationTests")]
public class UpdateTaskIntegrationTests
{
    private readonly PostgresFixture _fixture;

    public UpdateTaskIntegrationTests(PostgresFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Update_Task_EndToEnd()
    {
        // GIVEN
        var createRequest = new CreateTaskRequestBuilder().Build();

        var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

        var updateRequest = new
        {
            id = created!.Id,
            title = "Updated Title",
            description = "Updated Desc"
        };

        // WHEN
        var response = await _fixture.Client.PutAsJsonAsync($"/api/tasks/{created.Id}", updateRequest);

        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var dbTask = await GetDbTaskItem(created.Id);

        dbTask!.Title.Should().Be(updateRequest.title);
        dbTask.Description.Should().Be(updateRequest.description);
    }

    [Fact]
    public async Task Should_Return_404_When_Task_Does_Not_Exist()
    {
        var request = new
        {
            id = Guid.NewGuid(),
            title = "Title",
            description = "Desc"
        };

        var response = await _fixture.Client.PutAsJsonAsync($"/api/tasks/{request.id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_Return_400_When_Title_Is_Empty()
    {
        var create = await _fixture.Client.PostAsJsonAsync("/api/tasks",
            new CreateTaskRequestBuilder().Build());

        var created = await create.Content.ReadFromJsonAsync<CreateTaskResponse>();

        var request = new
        {
            id = created!.Id,
            title = "",
            description = "Desc"
        };

        var response = await _fixture.Client.PutAsJsonAsync($"/api/tasks/{created.Id}", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task<TaskItem?> GetDbTaskItem(Guid id)
    {
        using var scope = _fixture.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await db.Tasks.FindAsync(id);
    }
}