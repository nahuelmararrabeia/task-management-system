using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.CreateTask;
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

    private async Task<TaskItem?> GetDbTaskItem(Guid id)
    {
        using var scope = _fixture.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await db.Tasks.FindAsync(id);
    }
}