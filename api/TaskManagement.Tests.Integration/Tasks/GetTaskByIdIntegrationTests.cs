using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Tasks;

[Collection("IntegrationTests")]
public class GetTaskByIdIntegrationTests
{
    private readonly PostgresFixture _fixture;

    public GetTaskByIdIntegrationTests(PostgresFixture fixture)
    {
        _fixture = fixture;
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

    [Fact]
    public async Task Should_Return_404_When_Getting_Deleted_Task()
    {
        // GIVEN
        var createRequest = new CreateTaskRequestBuilder().Build();

        var createResponse = await _fixture.Client.PostAsJsonAsync("/api/tasks", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskResponse>();

        await _fixture.Client.DeleteAsync($"/api/tasks/{created!.Id}");

        // WHEN
        var response = await _fixture.Client.GetAsync($"/api/tasks/{created.Id}");

        // THEN
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}