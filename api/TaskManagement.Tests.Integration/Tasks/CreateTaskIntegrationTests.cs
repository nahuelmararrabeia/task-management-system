using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks;

[Collection("IntegrationTests")]
public class CreateTaskIntegrationTests
{
    private readonly PostgresFixture _fixture;

    public CreateTaskIntegrationTests(PostgresFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Create_Task_EndToEnd()
    {
        // GIVEN
        var request = new CreateTaskRequestBuilder().Build();

        // WHEN
        var response = await _fixture.Client.PostAsJsonAsync("/api/tasks", request);

        // THEN
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<CreateTaskResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().NotBe(Guid.Empty);

        var dbTask = await GetDbTaskItem(result.Id);

        dbTask.Should().NotBeNull();
        dbTask!.Title.Should().Be(request.Title);
        dbTask.Description.Should().Be(request.Description);
    }

    [Fact]
    public async Task Should_Get_Task_By_Id()
    {
        // GIVEN
        var createRequest = new CreateTaskRequestBuilder().Build();
        var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createRequest);
        var createdTask = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

        // WHEN
        var response = await _fixture.Client.GetAsync($"/api/tasks/{createdTask!.Id}");

        // THEN
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GetTaskByIdResponse>();

        result.Should().NotBeNull();
        result!.Title.Should().Be(createRequest.Title);
        result!.Description.Should().Be(createRequest.Description);
    }

    [Fact]
    public async Task Should_Return_404_When_Task_Does_Not_Exist()
    {
        // GIVEN
        var nonExistentId = Guid.NewGuid();

        // WHEN
        var response = await _fixture.Client.GetAsync($"/api/tasks/{nonExistentId}");

        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<TaskItem?> GetDbTaskItem(Guid id)
    {
        using var scope = _fixture.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await db.Tasks.FindAsync(id);
    }
}