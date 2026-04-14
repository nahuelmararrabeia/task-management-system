using TaskManagement.Tests.Integration.Fixtures;

[CollectionDefinition("IntegrationTests")]
public class IntegrationTestCollection : ICollectionFixture<PostgresFixture>
{
}