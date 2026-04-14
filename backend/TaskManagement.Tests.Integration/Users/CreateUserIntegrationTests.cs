using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TaskManagement.Application.Users.Commands.CreateUser;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Tests.Integration.Fixtures;
using TaskManagement.Tests.Integration.TestData.Builders;

namespace TaskManagement.Tests.Integration.Users
{
    [Collection("IntegrationTests")]
    public class CreateUserIntegrationTests
    {
        private readonly PostgresFixture _fixture;

        public CreateUserIntegrationTests(PostgresFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Create_User()
        {
            // GIVEN
            var request = new CreateUserRequestBuilder().Build();

            // WHEN
            var response = await _fixture.Client.PostAsJsonAsync("/api/user", request);

            // THEN
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateUserResponse>();

            result.Should().NotBeNull();
            result!.Id.Should().NotBe(Guid.Empty);

            using var scope = _fixture.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var user = await db.Users.FindAsync(result.Id);

            user.Should().NotBeNull();
            user!.Email.Should().Be(request.Email);
            user.Name.Should().Be(request.Name);

            user.PasswordHash.Should().NotBeNullOrEmpty();
            user.PasswordHash.Should().NotBe(request.Password);

            var hasher = new PasswordHasher<User>();

            var verify = hasher.VerifyHashedPassword(
                null!,
                user!.PasswordHash,
                request.Password
            );

            verify.Should().Be(PasswordVerificationResult.Success);
        }

        [Fact]
        public async Task Should_Return_409_When_Email_Already_Exists()
        {
            // GIVEN
            var request = new CreateUserRequestBuilder().Build();

            await _fixture.Client.PostAsJsonAsync("/api/user", request);

            // WHEN
            var response = await _fixture.Client.PostAsJsonAsync("/api/user", request);

            // THEN
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Should_Return_400_When_Email_Invalid()
        {
            // GIVEN
            var request = new CreateUserRequestBuilder()
                .WithEmail("invalid-email")
                .Build();

            // WHEN
            var response = await _fixture.Client.PostAsJsonAsync("/api/user", request);

            // THEN
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
